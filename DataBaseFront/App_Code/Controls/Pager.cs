using System;
using System.Windows.Forms;

namespace DataBaseFront.Controls
{
    /// <summary>
    /// 申明委托
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate int EventPagingHandler(EventPagingArg e);

    /// <summary>
    /// 分页控件呈现
    /// </summary>
    public partial class Pager : UserControl
    {
        public Pager()
        {
            InitializeComponent();

            this.Size = this.bindingNavigator1.Size;
        }

        public event EventPagingHandler EventPaging;

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int _pageSize = 50;

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                GetPageCount();
            }
        }

        private int _nMax = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int NMax
        {
            get { return _nMax; }
            set
            {
                _nMax = value;
                GetPageCount();
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _pageIndex = 0;

        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 设置页面大小
        /// </summary>
        private void GetPageCount()
        {
            if (this.NMax > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.NMax) / Convert.ToDouble(this.PageSize)));
                lblPageCount.Text = " / " + PageCount.ToString();
                lblPageSize.Text = "每页 " + PageSize.ToString() + " 条，共 " + PageCount.ToString() + " 页";
            }
            else
            {
                this.PageCount = 0;
            }
        }


        /// <summary>
        /// 翻页控件数据绑定
        /// </summary>
        public void Bind()
        {
            if (this.EventPaging != null)
            {
                this.NMax = this.EventPaging(new EventPagingArg(this.PageIndex));
            }

            if (this.PageIndex > this.PageCount)
            {
                this.PageIndex = this.PageCount;
            }

            if (this.PageCount == 1)
            {
                this.PageIndex = 1;
            }

            lblcurentpage.Text = PageIndex.ToString();
            lblRecordCount.Text = "共 " + NMax.ToString() + " 条记录";

            btnPrev.Enabled = true;
            btnFirst.Enabled = true;
            btnLast.Enabled = true;
            btnNext.Enabled = true;

            if (this.PageIndex == 1)
            {
                this.btnPrev.Enabled = false;
                this.btnFirst.Enabled = false;
            }


            if (this.PageIndex == this.PageCount)
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }

            if (this.NMax == 0)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
            }
            cmbPagecount.Items.Clear();
            for (int i = 1; i <= PageCount; i++)
                cmbPagecount.Items.Add(i.ToString());
            cmbPagecount.SelectedIndex = PageIndex - 1;

        }

        /// <summary>
        /// 首页
        /// </summary>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            this.Bind();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }
            this.Bind();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.PageIndex += 1;
            if (PageIndex > PageCount)
            {
                PageIndex = PageCount;
            }
            this.Bind();
        }

        /// <summary>
        /// 最后页
        /// </summary>
        private void btnLast_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount;
            this.Bind();
        }

        /// <summary>
        /// 转到新页
        /// </summary>
        public void btnGo_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(cmbPagecount.SelectedItem.ToString(), out _pageIndex))
            {
                this.Bind();
            }
        }
    }

    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    public class EventPagingArg : EventArgs
    {
        private int _intPageIndex;
        public EventPagingArg(int PageIndex)
        {
            _intPageIndex = PageIndex;
        }
    }
}
