<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>$DataBaseName 数据库结构</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        html{font-size:62.5%;font-family:Tahoma}
        .main { position:relative;}
        .contentLeft{position:fixed; width:180px; padding:6px 10px; height:90%; overflow-y:auto; border:1px solid #ccc; }
        .contentLeft a{color:#5f940a; font-weight:bold; font-size:14px;}
        .contentLeft ul {margin:0px; padding:0px;}
        .contentLeft li{list-style:none; list-style-type:none; float:left; width:150px; line-height:150%;}
        .contentRight {padding:10px 10px 10px 210px; }
        .contentRight a{color:#5f940a;}
        .contentRight h1,.contentRight h2{font-size:14px;font-weight:bold;color:#333;line-height:25px;height:25px;margin-bottom:10px;margin-left:0;border-radius:3px;-moz-border-radius:3px;-webkit-border-radius:3px;overflow:hidden}
        .contentRight h3,.contentRight h4{font-size:14px;font-weight:bold;color:#333;line-height:25px;height:25px;margin-top:10px;overflow:hidden}
        div.contentSub{font-family:"Microsoft Yahei",helvetica,'Lucida Grande',Tahoma,Verdana,Simsun,Arial,Clean;background-color:#fff;text-align:left;margin:0 auto}
        div#bodyContent p,div#bodyContent pre{font-family:Tahoma,"SimSun";line-height:24px;font-size:12px}
        div#bodyContent p img{margin-top:6px;margin-bottom:6px}
        div#bodyContent table.t2{width:720px;border:1px solid #DBDBDB}
        div#bodyContent table.t2 tr td,div#bodyContent table.t2 tr td *{line-height:18px}
        div#bodyContent table.t2 td,table.t2 th{border-right:1px solid #DBDBDB;border-bottom:1px solid #DBDBDB;color:#000}
        div#bodyContent table.t2 td{padding:3px 0;padding-left:10px}
        div#bodyContent table.t2 th{background:-webkit-gradient(linear,0 0,0 100%,from(#616161),to(#555));background:-moz-linear-gradient(top,#616161,#555);background:-ms-linear-gradient(top,#616161,#555);background:-o-linear-gradient(top,#616161,#555);filter:progid:DXImageTransform.Microsoft.Gradient(startColorStr='#616161', endColorStr='#555555',gradientType='0');*background-color:#616161;color:#fff;border:#4A4A4A solid 1px;height:36px;text-align:center;font-weight:normal}
    </style>
</head>
<body>
    <div class="main">
        <div class="contentLeft">
            <ul>
#foreach( $table in $Tables )
    <li><a href="#${table.Name}">${table.Name}</a></li>
#end
            </ul>
        </div>
        <div class="contentRight">
#foreach( $tableinfo in $TableInfos )
            <div id="${tableinfo.Table.Name}">
                <h2>${tableinfo.Table.Name}</h2>
                <div id="bodyContent">
                    <table class="t2">
                        <tr>
                            <th width="40"><b>序号</b></th>
                            <th width="140"><b>字段名</b></th>
                            <th width="200"><b>字段说明</b></th>
                            <th width="120"><b>自动增长</b></th>
                            <th width="60"><b>主键</b></th>
                            <th width="60"><b>类型</b></th>
                            <th width="60"><b>长度</b></th>
                            <th width="60"><b>精度</b></th>
                            <th width="60"><b>小数位</b></th>
                            <th width="60"><b>允许空</b></th>
                            <th width="120"><b>默认值</b></th>
                        </tr>
    #foreach( $columnInfo in $tableinfo.Columns )
                        <tr>
                            <td>${columnInfo.Index}</td>
                            <td>${columnInfo.Name}</td>
                            <td>${columnInfo.Remark}</td>
                            <td> #if ($columnInfo.AutoIncrement) 是 #else 否 #end</td>
                            <td> #if ($columnInfo.IsPrimaryKey) 是 #else 否 #end</td>
                            <td>${columnInfo.DbType}</td>
                            <td>${columnInfo.Length}</td>
                            <td>${columnInfo.NumericPrecision}</td>
                            <td>${columnInfo.NumericScale}</td>
                            <td> #if ($columnInfo.AllowNull) 是 #else 否 #end</td>
                            <td>${columnInfo.DefaultValue}</td>
                        </tr>
    #end
                    </table>
                </div>
            </div>
#end
        </div>
    </div>
</body>
</html>