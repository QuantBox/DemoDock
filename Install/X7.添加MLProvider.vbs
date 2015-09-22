'加载目标文件
Set oShell = CreateObject("WScript.Shell")
strHomeFolder = oShell.ExpandEnvironmentStrings("%APPDATA%")
'wscript.echo strHomeFolder

OldConfigFileName = strHomeFolder &"\SmartQuant Ltd\OpenQuant 2014\config\configuration.xml"
NewConfigFileName = strHomeFolder &"\SmartQuant Ltd\OpenQuant 2014\config\configuration.xml"

Set xmlDoc = CreateObject("Microsoft.XMLDOM")
xmlDoc.async= false 
xmlDoc.load(OldConfigFileName)

' 找到Providers节点
Set Providers = xmlDoc.selectSingleNode("/Configuration/Providers")

Set WshShell = WScript.CreateObject("WScript.Shell")

' 找到Provider节点
Set Providers_Provider = xmlDoc.selectSingleNode("/Configuration/Providers/Provider[TypeName='MenuLoader.MLProvider, MenuLoader']")
If Providers_Provider is Nothing Then
	BtnCode = WshShell.Popup("没有找到节点，是否添加？", 7, "添加", 32+4)
	If BtnCode = 6 Then
		AddNode(Providers)
	End if
Else
	
	BtnCode = WshShell.Popup("节点已经存在，是否删除？", 7, "删除", 32+4)
	If BtnCode = 6 Then
		Providers.removeChild(Providers_Provider)
	End if
End If

' 保存
If BtnCode = 6 Then
	xmlDoc.Save NewConfigFileName
	MsgBox("保存成功")
End if


'创建插件节点并加入
Sub AddNode(parent)  
	Set Child1 = xmlDoc.createElement("TypeName") 
	Child1.Text = "MenuLoader.MLProvider, MenuLoader"
	Set Child2 = xmlDoc.createElement("X64")
	Child2.Text = "true"
	Set Child0 = xmlDoc.createElement("Provider")
	Child0.appendChild Child1
	Child0.appendChild Child2
	parent.appendChild Child0
End Sub