﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3603
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=2.0.50727.3038.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(), _
 System.Xml.Serialization.XmlRootAttribute()> _
Partial Public Class Job

    Private jobNumberField As Integer

    Private jobItemField() As JobItem

    Private designIdField As Integer

    Private itemField As Object

    '''<remarks/>
    Public Property jobNumber() As Integer
        Get
            Return Me.jobNumberField
        End Get
        Set(ByVal value As Integer)
            Me.jobNumberField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("JobItem")> _
    Public Property JobItem() As JobItem()
        Get
            Return Me.jobItemField
        End Get
        Set(ByVal value As JobItem())
            Me.jobItemField = value
        End Set
    End Property

    '''<remarks/>
    Public Property DesignId() As Integer
        Get
            Return Me.designIdField
        End Get
        Set(ByVal value As Integer)
            Me.designIdField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("GeoFrame", GetType(GeoFrame)), _
     System.Xml.Serialization.XmlElementAttribute("Pattern", GetType(Pattern))> _
    Public Property Item() As Object
        Get
            Return Me.itemField
        End Get
        Set(ByVal value As Object)
            Me.itemField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class JobItem

    Private jobSideField As String

    Private blankField As Blank

    Private lensField As Lens

    '''<remarks/>
    Public Property jobSide() As String
        Get
            Return Me.jobSideField
        End Get
        Set(ByVal value As String)
            Me.jobSideField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Blank() As Blank
        Get
            Return Me.blankField
        End Get
        Set(ByVal value As Blank)
            Me.blankField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property Lens() As Lens
        Get
            Return Me.lensField
        End Get
        Set(ByVal value As Lens)
            Me.lensField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Blank

    Private itemField As Object

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("LensDescr", GetType(LensDescr)), _
     System.Xml.Serialization.XmlElementAttribute("opc", GetType(String))> _
    Public Property Item() As Object
        Get
            Return Me.itemField
        End Get
        Set(ByVal value As Object)
            Me.itemField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class LensDescr

    Private matNameField As String

    Private nomFCurveField As Double

    Private nomFCurveFieldSpecified As Boolean

    Private matVCAField As Integer

    Private diamField As Double

    Private frontCurveField As Double

    Private backCurveField As Double

    Private cenThickField As Double

    Private matIndexField As Double

    Private minCThkField As Double

    '''<remarks/>
    Public Property matName() As String
        Get
            Return Me.matNameField
        End Get
        Set(ByVal value As String)
            Me.matNameField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property nomFCurve() As Double
        Get
            Return Me.nomFCurveField
        End Get
        Set(ByVal value As Double)
            Me.nomFCurveField = Value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property nomFCurveSpecified() As Boolean
        Get
            Return Me.nomFCurveFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.nomFCurveFieldSpecified = Value
        End Set
    End Property

    '''<remarks/>
    Public Property matVCA() As Integer
        Get
            Return Me.matVCAField
        End Get
        Set(ByVal value As Integer)
            Me.matVCAField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property diam() As Double
        Get
            Return Me.diamField
        End Get
        Set(ByVal value As Double)
            Me.diamField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property frontCurve() As Double
        Get
            Return Me.frontCurveField
        End Get
        Set(ByVal value As Double)
            Me.frontCurveField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property backCurve() As Double
        Get
            Return Me.backCurveField
        End Get
        Set(ByVal value As Double)
            Me.backCurveField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property cenThick() As Double
        Get
            Return Me.cenThickField
        End Get
        Set(ByVal value As Double)
            Me.cenThickField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property matIndex() As Double
        Get
            Return Me.matIndexField
        End Get
        Set(ByVal value As Double)
            Me.matIndexField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property minCThk() As Double
        Get
            Return Me.minCThkField
        End Get
        Set(ByVal value As Double)
            Me.minCThkField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Thickness

    Private thkField As Double

    Private angleField As Double

    '''<remarks/>
    Public Property thk() As Double
        Get
            Return Me.thkField
        End Get
        Set(ByVal value As Double)
            Me.thkField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property angle() As Double
        Get
            Return Me.angleField
        End Get
        Set(ByVal value As Double)
            Me.angleField = Value
        End Set
    End Property
End Class

'''<remarks/>
''' 
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class BlockCurve

    Private bCNameField As String

    Private curveField As Double

    '''<remarks/>
    Public Property blockCurveName() As String
        Get
            Return Me.bCNameField
        End Get
        Set(ByVal value As String)
            Me.bCNameField = value
        End Set
    End Property

    Public Property Curve() As Double
        Get
            Return Me.curveField
        End Get
        Set(ByVal value As Double)
            Me.curveField = value
        End Set
    End Property
End Class


<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Block

    Private blockNameField As String

    Private diameterField As Double

    Private edgeHeigthField As Double

    Private blockCurveField As BlockCurve


    '''<remarks/>
    Public Property blockCurve() As BlockCurve
        Get
            Return Me.blockCurveField
        End Get
        Set(ByVal value As BlockCurve)
            Me.blockCurveField = value
        End Set
    End Property
    '''<remarks/>
    Public Property blockName() As String
        Get
            Return Me.blockNameField
        End Get
        Set(ByVal value As String)
            Me.blockNameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property diameter() As Double
        Get
            Return Me.diameterField
        End Get
        Set(ByVal value As Double)
            Me.diameterField = value
        End Set
    End Property

    '''<remarks/>
    Public Property edgeHeigth() As Double
        Get
            Return Me.edgeHeigthField
        End Get
        Set(ByVal value As Double)
            Me.edgeHeigthField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class CurvePower

    Private referenceIndexField As Double

    Private referenceIndexFieldSpecified As Boolean

    Private powerField As Double

    '''<remarks/>
    Public Property referenceIndex() As Double
        Get
            Return Me.referenceIndexField
        End Get
        Set(ByVal value As Double)
            Me.referenceIndexField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property referenceIndexSpecified() As Boolean
        Get
            Return Me.referenceIndexFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.referenceIndexFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property power() As Double
        Get
            Return Me.powerField
        End Get
        Set(ByVal value As Double)
            Me.powerField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Profiles

    Private baseField As CurvePower

    Private crossField As CurvePower

    '''<remarks/>
    Public Property base() As CurvePower
        Get
            Return Me.baseField
        End Get
        Set(ByVal value As CurvePower)
            Me.baseField = value
        End Set
    End Property

    '''<remarks/>
    Public Property cross() As CurvePower
        Get
            Return Me.crossField
        End Get
        Set(ByVal value As CurvePower)
            Me.crossField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class LensDsg

    Private sideField As String

    Private genSurfField As Profiles

    Private lapSurfField As Profiles

    Private lapSurfField2 As Profiles

    Private ringSurfField As Block

    Private prismField As Prism

    Private cenThkField As Double

    Private minEThkField As Thickness

    Private maxEthkField As Thickness



    '''<remarks/>
    Public Property side() As String
        Get
            Return Me.sideField
        End Get
        Set(ByVal value As String)
            Me.sideField = value
        End Set
    End Property

    '''<remarks/>
    Public Property genSurf() As Profiles
        Get
            Return Me.genSurfField
        End Get
        Set(ByVal value As Profiles)
            Me.genSurfField = value
        End Set
    End Property

    '''<remarks/>
    Public Property lapSurf() As Profiles
        Get
            Return Me.lapSurfField
        End Get
        Set(ByVal value As Profiles)
            Me.lapSurfField = value
        End Set
    End Property
    Public Property lapSurf2() As Profiles
        Get
            Return Me.lapSurfField2
        End Get
        Set(ByVal value As Profiles)
            Me.lapSurfField2 = value
        End Set
    End Property

    '''<remarks/>
    Public Property ringSurf() As Block
        Get
            Return Me.ringSurfField
        End Get
        Set(ByVal value As Block)
            Me.ringSurfField = value
        End Set
    End Property

    '''<remarks/>
    Public Property Prism() As Prism
        Get
            Return Me.prismField
        End Get
        Set(ByVal value As Prism)
            Me.prismField = value
        End Set
    End Property

    '''<remarks/>
    Public Property cenThk() As Double
        Get
            Return Me.cenThkField
        End Get
        Set(ByVal value As Double)
            Me.cenThkField = value
        End Set
    End Property

    '''<remarks/>
    Public Property minEThk() As Thickness
        Get
            Return Me.minEThkField
        End Get
        Set(ByVal value As Thickness)
            Me.minEThkField = value
        End Set
    End Property

    '''<remarks/>
    Public Property maxEthk() As Thickness
        Get
            Return Me.maxEthkField
        End Get
        Set(ByVal value As Thickness)
            Me.maxEthkField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Prism

    Private diopField As Double

    Private axisField As Double

    '''<remarks/>
    Public Property diop() As Double
        Get
            Return Me.diopField
        End Get
        Set(ByVal value As Double)
            Me.diopField = value
        End Set
    End Property

    '''<remarks/>
    Public Property axis() As Double
        Get
            Return Me.axisField
        End Get
        Set(ByVal value As Double)
            Me.axisField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class DistXY

    Private xField As Double

    Private yField As Double

    '''<remarks/>
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set(ByVal value As Double)
            Me.xField = value
        End Set
    End Property

    '''<remarks/>
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set(ByVal value As Double)
            Me.yField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class DisplPattern

    Private sideField As String

    Private diamField As Integer

    Private cribField As Double

    Private heigthField As Single

    Private heigthFieldSpecified As Boolean

    Private displField As DistXY

    Private descenteringField As DistXY

    Private lRPField As DistXY

    Private segWdField As Double

    Private noticeField As String

    Private segWdFieldSpecified As Boolean

    Private segHtField As Double

    Private segHtFieldSpecified As Boolean

    '''<remarks/>
    Public Property side() As String
        Get
            Return Me.sideField
        End Get
        Set(ByVal value As String)
            Me.sideField = value
        End Set
    End Property

    '''<remarks/>
    Public Property diam() As Integer
        Get
            Return Me.diamField
        End Get
        Set(ByVal value As Integer)
            Me.diamField = value
        End Set
    End Property

    '''<remarks/>
    Public Property crib() As Double
        Get
            Return Me.cribField
        End Get
        Set(ByVal value As Double)
            Me.cribField = value
        End Set
    End Property

    '''<remarks/>
    Public Property heigth() As Single
        Get
            Return Me.heigthField
        End Get
        Set(ByVal value As Single)
            Me.heigthField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property heigthSpecified() As Boolean
        Get
            Return Me.heigthFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.heigthFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property displ() As DistXY
        Get
            Return Me.displField
        End Get
        Set(ByVal value As DistXY)
            Me.displField = value
        End Set
    End Property

    '''<remarks/>
    Public Property descentering() As DistXY
        Get
            Return Me.descenteringField
        End Get
        Set(ByVal value As DistXY)
            Me.descenteringField = value
        End Set
    End Property

    '''<remarks/>
    Public Property LRP() As DistXY
        Get
            Return Me.lRPField
        End Get
        Set(ByVal value As DistXY)
            Me.lRPField = value
        End Set
    End Property

    '''<remarks/>
    Public Property segWd() As Double
        Get
            Return Me.segWdField
        End Get
        Set(ByVal value As Double)
            Me.segWdField = value
        End Set
    End Property

    '''<remarks/>
    Public Property notice() As String
        Get
            Return Me.noticeField
        End Get
        Set(ByVal value As String)
            Me.noticeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property segWdSpecified() As Boolean
        Get
            Return Me.segWdFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.segWdFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property segHt() As Double
        Get
            Return Me.segHtField
        End Get
        Set(ByVal value As Double)
            Me.segHtField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property segHtSpecified() As Boolean
        Get
            Return Me.segHtFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.segHtFieldSpecified = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Pattern

    Private frameField As Frame

    Private sizingField As Integer

    Private curveField As Double

    Private radField As String

    '''<remarks/>
    Public Property Frame() As Frame
        Get
            Return Me.frameField
        End Get
        Set(ByVal value As Frame)
            Me.frameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property sizing() As Integer
        Get
            Return Me.sizingField
        End Get
        Set(ByVal value As Integer)
            Me.sizingField = value
        End Set
    End Property

    '''<remarks/>
    Public Property curve() As Double
        Get
            Return Me.curveField
        End Get
        Set(ByVal value As Double)
            Me.curveField = value
        End Set
    End Property

    '''<remarks/>
    Public Property rad() As String
        Get
            Return Me.radField
        End Get
        Set(ByVal value As String)
            Me.radField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Frame

    Private frameTypeField As Integer

    Private dblField As Double

    Private distPDField As Double

    Private nearPDField As Double

    '''<remarks/>
    Public Property frameType() As Integer
        Get
            Return Me.frameTypeField
        End Get
        Set(ByVal value As Integer)
            Me.frameTypeField = value
        End Set
    End Property

    '''<remarks/>
    Public Property dbl() As Double
        Get
            Return Me.dblField
        End Get
        Set(ByVal value As Double)
            Me.dblField = value
        End Set
    End Property

    '''<remarks/>
    Public Property distPD() As Double
        Get
            Return Me.distPDField
        End Get
        Set(ByVal value As Double)
            Me.distPDField = value
        End Set
    End Property

    '''<remarks/>
    Public Property nearPD() As Double
        Get
            Return Me.nearPDField
        End Get
        Set(ByVal value As Double)
            Me.nearPDField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class GeoFrame

    Private frameField As Frame

    Private hrzBoxField As Double

    Private vrtBoxField As Double

    Private efDiamField As Double

    Private edAxisField As Double

    Private edAxisFieldSpecified As Boolean

    '''<remarks/>
    Public Property Frame() As Frame
        Get
            Return Me.frameField
        End Get
        Set(ByVal value As Frame)
            Me.frameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property hrzBox() As Double
        Get
            Return Me.hrzBoxField
        End Get
        Set(ByVal value As Double)
            Me.hrzBoxField = value
        End Set
    End Property

    '''<remarks/>
    Public Property vrtBox() As Double
        Get
            Return Me.vrtBoxField
        End Get
        Set(ByVal value As Double)
            Me.vrtBoxField = value
        End Set
    End Property

    '''<remarks/>
    Public Property efDiam() As Double
        Get
            Return Me.efDiamField
        End Get
        Set(ByVal value As Double)
            Me.efDiamField = value
        End Set
    End Property

    '''<remarks/>
    Public Property edAxis() As Double
        Get
            Return Me.edAxisField
        End Get
        Set(ByVal value As Double)
            Me.edAxisField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property edAxisSpecified() As Boolean
        Get
            Return Me.edAxisFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.edAxisFieldSpecified = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Lens

    Private sphereField As Double

    Private cylinderField As Double

    Private axisField As Double

    Private additionField As Double

    Private prismField As Prism

    Private prThinOkField As Boolean

    Private prThinOkFieldSpecified As Boolean

    Private cribField As Double

    Private cribFieldSpecified As Boolean

    Private cenThickField As Double

    Private cenThickFieldSpecified As Boolean

    Private edgThickField As Double

    Private edgThickFieldSpecified As Boolean

    Private monoPDField As Double

    Private monoPDFieldSpecified As Boolean

    Private segHeigthField As Double

    Private segHeigthFieldSpecified As Boolean

    '''<remarks/>
    Public Property sphere() As Double
        Get
            Return Me.sphereField
        End Get
        Set(ByVal value As Double)
            Me.sphereField = value
        End Set
    End Property

    '''<remarks/>
    Public Property cylinder() As Double
        Get
            Return Me.cylinderField
        End Get
        Set(ByVal value As Double)
            Me.cylinderField = value
        End Set
    End Property

    '''<remarks/>
    Public Property axis() As Double
        Get
            Return Me.axisField
        End Get
        Set(ByVal value As Double)
            Me.axisField = value
        End Set
    End Property

    '''<remarks/>
    Public Property addition() As Double
        Get
            Return Me.additionField
        End Get
        Set(ByVal value As Double)
            Me.additionField = value
        End Set
    End Property

    '''<remarks/>
    Public Property Prism() As Prism
        Get
            Return Me.prismField
        End Get
        Set(ByVal value As Prism)
            Me.prismField = value
        End Set
    End Property

    '''<remarks/>
    Public Property prThinOk() As Boolean
        Get
            Return Me.prThinOkField
        End Get
        Set(ByVal value As Boolean)
            Me.prThinOkField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property prThinOkSpecified() As Boolean
        Get
            Return Me.prThinOkFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.prThinOkFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property crib() As Double
        Get
            Return Me.cribField
        End Get
        Set(ByVal value As Double)
            Me.cribField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property cribSpecified() As Boolean
        Get
            Return Me.cribFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.cribFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property cenThick() As Double
        Get
            Return Me.cenThickField
        End Get
        Set(ByVal value As Double)
            Me.cenThickField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property cenThickSpecified() As Boolean
        Get
            Return Me.cenThickFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.cenThickFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property edgThick() As Double
        Get
            Return Me.edgThickField
        End Get
        Set(ByVal value As Double)
            Me.edgThickField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property edgThickSpecified() As Boolean
        Get
            Return Me.edgThickFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.edgThickFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property monoPD() As Double
        Get
            Return Me.monoPDField
        End Get
        Set(ByVal value As Double)
            Me.monoPDField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property monoPDSpecified() As Boolean
        Get
            Return Me.monoPDFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.monoPDFieldSpecified = value
        End Set
    End Property

    '''<remarks/>
    Public Property segHeigth() As Double
        Get
            Return Me.segHeigthField
        End Get
        Set(ByVal value As Double)
            Me.segHeigthField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()> _
    Public Property segHeigthSpecified() As Boolean
        Get
            Return Me.segHeigthFieldSpecified
        End Get
        Set(ByVal value As Boolean)
            Me.segHeigthFieldSpecified = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class DisplFrame

    Private displPatternField() As DisplPattern

    Private geoFrameField As GeoFrame

    Private patternField As Pattern

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("DisplPattern")> _
    Public Property DisplPattern() As DisplPattern()
        Get
            Return Me.displPatternField
        End Get
        Set(ByVal value As DisplPattern())
            Me.displPatternField = value
        End Set
    End Property

    '''<remarks/>
    Public Property GeoFrame() As GeoFrame
        Get
            Return Me.geoFrameField
        End Get
        Set(ByVal value As GeoFrame)
            Me.geoFrameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property Pattern() As Pattern
        Get
            Return Me.patternField
        End Get
        Set(ByVal value As Pattern)
            Me.patternField = value
        End Set
    End Property
End Class
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute()> _
Partial Public Class Displacement

    Private displPatternField() As DisplPattern

    Private geoFrameField As GeoFrame

    Private patternField As Pattern

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("DisplPattern")> _
    Public Property DisplPattern() As DisplPattern()
        Get
            Return Me.displPatternField
        End Get
        Set(ByVal value As DisplPattern())
            Me.displPatternField = value
        End Set
    End Property

    '''<remarks/>
    Public Property GeoFrame() As GeoFrame
        Get
            Return Me.geoFrameField
        End Get
        Set(ByVal value As GeoFrame)
            Me.geoFrameField = value
        End Set
    End Property

    '''<remarks/>
    Public Property Pattern() As Pattern
        Get
            Return Me.patternField
        End Get
        Set(ByVal value As Pattern)
            Me.patternField = value
        End Set
    End Property
End Class


'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(), _
 System.Xml.Serialization.XmlRootAttribute()> _
Partial Public Class LensDsgd

    Private lensDsgField() As LensDsg

    Private displPatternField() As DisplPattern

    Private geoFrameField As GeoFrame

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("LensDsg")> _
    Public Property LensDsg() As LensDsg()
        Get
            Return Me.lensDsgField
        End Get
        Set(ByVal value As LensDsg())
            Me.lensDsgField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("DisplPattern")> _
    Public Property DisplPattern() As DisplPattern()
        Get
            Return Me.displPatternField
        End Get
        Set(ByVal value As DisplPattern())
            Me.displPatternField = value
        End Set
    End Property

    '''<remarks/>
    Public Property GeoFrame() As GeoFrame
        Get
            Return Me.geoFrameField
        End Get
        Set(ByVal value As GeoFrame)
            Me.geoFrameField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(), _
 System.Xml.Serialization.XmlRootAttribute()> _
Partial Public Class AccReq

    Private dateField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("date")> _
    Public Property finaldate() As String
        Get
            Return Me.dateField
        End Get
        Set(ByVal value As String)
            Me.dateField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(), _
 System.Xml.Serialization.XmlRootAttribute()> _
Partial Public Class AccStatement

    Private AccStatusField As AccStatus()

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("AccStatus")> _
    Public Property AccStatus() As AccStatus()
        Get
            Return Me.AccStatusField
        End Get
        Set(ByVal value As AccStatus())
            Me.AccStatusField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(), _
 System.Xml.Serialization.XmlRootAttribute()> _
Partial Public Class AccStatus
    Private referenceField As String
    Private dateField As String
    Private desIdField As Integer
    Private desNameField As String
    Private buyedField As Integer
    Private usedField As Integer

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("reference")> _
    Public Property reference() As String
        Get
            Return Me.referenceField
        End Get
        Set(ByVal value As String)
            Me.referenceField = value
        End Set
    End Property
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("date")> _
    Public Property buydate() As String
        Get
            Return Me.dateField
        End Get
        Set(ByVal value As String)
            Me.dateField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlElementAttribute("desId")> _
    Public Property desId() As Integer
        Get
            Return Me.desIdField
        End Get
        Set(ByVal value As Integer)
            Me.desIdField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlElementAttribute("desName")> _
    Public Property desName() As String
        Get
            Return Me.desNameField
        End Get
        Set(ByVal value As String)
            Me.desNameField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlElementAttribute("buyed")> _
    Public Property buyed() As Integer
        Get
            Return Me.buyedField
        End Get
        Set(ByVal value As Integer)
            Me.buyedField = value
        End Set
    End Property
    <System.Xml.Serialization.XmlElementAttribute("used")> _
    Public Property used() As Integer
        Get
            Return Me.usedField
        End Get
        Set(ByVal value As Integer)
            Me.usedField = value
        End Set
    End Property

End Class