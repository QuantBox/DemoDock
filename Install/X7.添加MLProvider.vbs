'����Ŀ���ļ�
Set oShell = CreateObject("WScript.Shell")
strHomeFolder = oShell.ExpandEnvironmentStrings("%APPDATA%")
'wscript.echo strHomeFolder

OldConfigFileName = strHomeFolder &"\SmartQuant Ltd\OpenQuant 2014\config\configuration.xml"
NewConfigFileName = strHomeFolder &"\SmartQuant Ltd\OpenQuant 2014\config\configuration.xml"

Set xmlDoc = CreateObject("Microsoft.XMLDOM")
xmlDoc.async= false 
xmlDoc.load(OldConfigFileName)

' �ҵ�Providers�ڵ�
Set Providers = xmlDoc.selectSingleNode("/Configuration/Providers")

Set WshShell = WScript.CreateObject("WScript.Shell")

' �ҵ�Provider�ڵ�
Set Providers_Provider = xmlDoc.selectSingleNode("/Configuration/Providers/Provider[TypeName='MenuLoader.MLProvider, MenuLoader']")
If Providers_Provider is Nothing Then
	BtnCode = WshShell.Popup("û���ҵ��ڵ㣬�Ƿ���ӣ�", 7, "���", 32+4)
	If BtnCode = 6 Then
		AddNode(Providers)
	End if
Else
	
	BtnCode = WshShell.Popup("�ڵ��Ѿ����ڣ��Ƿ�ɾ����", 7, "ɾ��", 32+4)
	If BtnCode = 6 Then
		Providers.removeChild(Providers_Provider)
	End if
End If

' ����
If BtnCode = 6 Then
	xmlDoc.Save NewConfigFileName
	MsgBox("����ɹ�")
End if


'��������ڵ㲢����
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