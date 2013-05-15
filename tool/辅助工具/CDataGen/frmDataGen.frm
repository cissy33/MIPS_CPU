VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0"; "MSCOMCTL.OCX"
Begin VB.Form frmDataGen 
   Caption         =   "DataGen"
   ClientHeight    =   6705
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8595
   LinkTopic       =   "Form1"
   ScaleHeight     =   6705
   ScaleWidth      =   8595
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdOKNext 
      Caption         =   "确认并修改下一个"
      Height          =   495
      Left            =   6120
      TabIndex        =   18
      Top             =   2040
      Width           =   2295
   End
   Begin VB.CommandButton cmdOpen 
      Caption         =   "打开数据"
      Height          =   375
      Left            =   6480
      TabIndex        =   17
      Top             =   120
      Width           =   1575
   End
   Begin VB.TextBox txtID 
      Alignment       =   2  'Center
      Appearance      =   0  'Flat
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   3960
      TabIndex        =   16
      Top             =   1440
      Width           =   975
   End
   Begin CDataGen.CDlg CD1 
      Height          =   420
      Left            =   8040
      TabIndex        =   15
      Top             =   5640
      Visible         =   0   'False
      Width           =   420
      _ExtentX        =   741
      _ExtentY        =   741
   End
   Begin VB.CommandButton cmdSave 
      Caption         =   "生成数据"
      Height          =   495
      Left            =   3960
      TabIndex        =   14
      Top             =   6120
      Width           =   4455
   End
   Begin VB.CommandButton cmdEdit 
      Caption         =   "确认修改"
      Height          =   495
      Left            =   3960
      TabIndex        =   13
      Top             =   2040
      Width           =   2175
   End
   Begin VB.TextBox txtValue 
      Alignment       =   2  'Center
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   5640
      TabIndex        =   11
      Top             =   1440
      Width           =   2775
   End
   Begin VB.Frame Frame1 
      Caption         =   "进制"
      Height          =   615
      Left            =   3960
      TabIndex        =   6
      Top             =   720
      Width           =   4455
      Begin VB.OptionButton optScale 
         Caption         =   "Hex"
         Height          =   255
         Index           =   3
         Left            =   3480
         TabIndex        =   10
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton optScale 
         Caption         =   "Dec"
         Height          =   255
         Index           =   2
         Left            =   2280
         TabIndex        =   9
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton optScale 
         Caption         =   "Oct"
         Height          =   255
         Index           =   1
         Left            =   1200
         TabIndex        =   8
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton optScale 
         Caption         =   "Bin"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   7
         Top             =   240
         Width           =   735
      End
   End
   Begin MSComctlLib.ListView lstDatas 
      Height          =   5895
      Left            =   240
      TabIndex        =   5
      Top             =   720
      Width           =   3615
      _ExtentX        =   6376
      _ExtentY        =   10398
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   -1  'True
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      NumItems        =   0
   End
   Begin VB.CommandButton cmdGen 
      Caption         =   "生成"
      Height          =   375
      Left            =   4920
      TabIndex        =   4
      Top             =   120
      Width           =   1215
   End
   Begin VB.TextBox txtDepth 
      Alignment       =   2  'Center
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   3000
      TabIndex        =   2
      Text            =   "64"
      Top             =   120
      Width           =   1575
   End
   Begin VB.TextBox txtWidth 
      Alignment       =   2  'Center
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   720
      TabIndex        =   0
      Text            =   "8"
      Top             =   120
      Width           =   1575
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      Caption         =   "="
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   13.5
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   5160
      TabIndex        =   12
      Top             =   1480
      Width           =   165
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "深度"
      Height          =   195
      Left            =   2400
      TabIndex        =   3
      Top             =   120
      Width           =   360
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "位宽"
      Height          =   195
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   360
   End
End
Attribute VB_Name = "frmDataGen"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim iDepth As Long, iWidth As Long
Dim arr() As Long
Dim iScale As Long
Dim iValue As Long
Dim iSelIndex As Long

Sub RefreshData()
'On Error Resume Next
Dim I As Long
Dim lstItem As ListItem
    lstDatas.ListItems.Clear
    For I = 0 To iDepth - 1
        Set lstItem = lstDatas.ListItems.Add(, , I)
        lstItem.ListSubItems.Add , , GetValue(arr(I))
    Next
End Sub

Sub RefreshValue()
    txtValue.Text = GetValue(iValue)
End Sub

Private Sub cmdEdit_Click()
    arr(iSelIndex) = SetValue(txtValue.Text)
    RefreshData
End Sub

Private Sub cmdGen_Click()
Dim I As Long, j As Long
    iDepth = Val(txtDepth.Text)
    iWidth = Val(txtWidth.Text)
    ReDim arr(iDepth)
    For I = 0 To iDepth - 1
        arr(I) = 0 'Int(Rnd * 1000)
    Next
    RefreshData
End Sub

Private Sub cmdOKNext_Click()
On Error Resume Next
    arr(iSelIndex) = SetValue(txtValue.Text)
    iSelIndex = iSelIndex + 1
    txtID.Text = iSelIndex
    iValue = arr(iSelIndex)
    RefreshData
    RefreshValue
    txtValue.SetFocus
    txtValue.SelStart = 0
    txtValue.SelLength = 99
End Sub

Private Sub cmdOpen_Click()
On Error GoTo LERR
Dim I As Long, sum As Long
Dim s As String
    CD1.ShowOpen
    sum = 0
    Open CD1.Filename For Input As #1
        While Not EOF(1)
            Line Input #1, s
            sum = sum + 1
        Wend
    Close #1
    iDepth = sum
    txtDepth.Text = iDepth
    iWidth = Len(s)
    txtWidth.Text = iWidth
    ReDim arr(iDepth)
    Open CD1.Filename For Input As #1
        For I = 0 To sum - 1
            Line Input #1, s
            arr(I) = SetValue(s, 0)
        Next
    Close #1
    RefreshData
    Exit Sub
LERR:
    'MsgBox "文件打开失败"
End Sub

Private Sub cmdSave_Click()
On Error GoTo LERR
Dim I As Long
    CD1.ShowSave
    Open CD1.Filename For Output As #1
        For I = 0 To iDepth - 1
            Print #1, GetValue(arr(I), 0)
        Next
    Close #1
    
    MsgBox "文件保存成功"
    Exit Sub
    
LERR:
    'MsgBox "文件保存失败"
End Sub

Private Sub Form_Load()
    optScale(0).Value = True
    lstDatas.ColumnHeaders.Add , , "序号", 1000
    lstDatas.ColumnHeaders.Add , , "数据", 2000
    CD1.Filter = "*.txt|*.txt|*.dat|*.dat|*.*|*.*"
End Sub

Private Sub lstDatas_Click()
    iSelIndex = lstDatas.SelectedItem.Index - 1
    txtID.Text = iSelIndex
    iValue = arr(iSelIndex)
    RefreshValue
End Sub

Private Sub lstDatas_DblClick()
    txtValue.SetFocus
    txtValue.SelStart = 0
    txtValue.SelLength = 99
End Sub

Private Sub optScale_Click(Index As Integer)
    iValue = SetValue(txtValue.Text)
    iScale = Index
    RefreshData
    RefreshValue
End Sub

Function SetValue(aValue As String, Optional aScale As Long = -1) As Long
On Error Resume Next
    If aScale = -1 Then aScale = iScale
    Select Case aScale
    Case 0:
        SetValue = BIN_to_DEC(aValue)
    Case 1:
        SetValue = Val("&O" & aValue)
    Case 2:
        SetValue = Val(aValue)
    Case 3:
        SetValue = Val("&H" & aValue)
    End Select
    
End Function

Function GetValue(aValue As Long, Optional aScale As Long = -1) As String
Dim lZero As String
Dim lWidth As Long
    If aScale = -1 Then aScale = iScale
    lZero = Replace(Space(iWidth), " ", "0")
    
    Select Case aScale
    Case 0:
        lWidth = iWidth
        GetValue = Right(lZero & Bin(aValue), lWidth)
    Case 1:
        lWidth = Ceil(iWidth / 3)
        GetValue = Right(lZero & Oct(aValue), lWidth)
    Case 2:
        lWidth = Ceil(iWidth / 3)
        GetValue = Right(lZero & aValue, lWidth)
    Case 3:
        lWidth = Ceil(iWidth / 4)
        GetValue = Right(lZero & Hex(aValue), lWidth)
    End Select
End Function

Public Function Bin(ByVal Dec As Long) As String
    Bin = ""
    Do While Dec > 0
        Bin = Dec Mod 2 & Bin
        Dec = Dec \ 2
    Loop
End Function

Public Function BIN_to_DEC(ByVal Bin As String) As Long
     Dim I As Long
     For I = 1 To Len(Bin)
         BIN_to_DEC = BIN_to_DEC * 2 + Val(Mid(Bin, I, 1))
     Next I
End Function

Public Function Ceil(aValue As Double) As Long
    Ceil = -Int(-aValue)
End Function

