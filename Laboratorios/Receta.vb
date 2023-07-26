Public Class Receta
    Public Custnum As Integer
    Public OriginalCustnum As Integer
    Public PONum As String
    Public RefDigital As Integer
    Public OriginalRefDigital As Integer
    Public Vantage As Integer
    Public IsManual As Boolean
    Public TipoArmazon As Frame_Types
    Public IsGarantia As Boolean
    Public IsGratis As Boolean
    Public A As Single
    Public B As Single
    Public ED As Single
    Public Puente As Single
    Public EsferaL As Single
    Public CilindroL As Single
    Public EjeL As Int16
    Public AdicionL As Single
    Public EsferaR As Single
    Public CilindroR As Single
    Public EjeR As Int16
    Public AdicionR As Single
    Public PrismaL As Single
    Public EjePrismaL As Int16
    Public PrismaR As Single
    Public EjePrismaR As Int16
    Public AlturaL As Single
    Public AlturaR As Single
    Public Altura As Single
    Public MonoL As Single
    Public MonoR As Single
    Public DIPLejos As Single
    Public DIPCerca As Single
    Public Biselado As Boolean
    Public AR As AR_Type
    Public IsVirtual As Boolean
    Public VirtualLab As Int16
    Public RLX As Boolean
    Public Pulido As Boolean
    Public Tinte As Int16
    Public Nivel As Int16
    Public Gradiente As Boolean
    Public Comentarios As String
    Public Action As Rx_Actions
    Public IsWebRx As Boolean
    Public NumArmazon As Integer
    Public COSTCO_Lente As Integer
    Public COSTCO_Abrillantado As Integer
    Public COSTCO_Tinte As Integer
    Public COSTCO_AR As Integer
    Public FechaEntradaWeb As String
    Public FechaEntradaFisica As String
    Public FechaOrden As String
    Public FechaEnvioVirtual As String
    Public FechaRecibeVirtual As String
    Public FechaSL As String
    Public FechaAR As String
    Public FechaFL As String
    Public FechaRecibeLocal As String
    Public FechaDebeTerminar As String
    Public FechaCierre As String
    Public FechaFactura As String
    Public FechaCobranza As String
    Public RxInVirtualLab As Integer
    Public POYear As Integer
    Public CouponID As String
    Public Subtotal As Single
    Public SeqID As Integer
    Public Cerrada As Boolean
    Public Facturada As Boolean
    Public Cobrada As Boolean
    Public Retenida As Boolean
    Public Reproceso As Boolean
    Public RxReceivedVirtual As Boolean
    Public RxReleasedVirtual As Boolean
    Public RxReceivedLocal As Boolean


    Enum AR_Type
        NoAR = 0
        AR_AUGEN = 1
        AR_GOLD = 2
    End Enum
    Enum Rx_Actions
        Create = 0
        Update = 1
        LocalReceive = 2
        VirtualReceive = 3
        Retention = 4
    End Enum
    Enum Frame_Types
        Metal = 1
        Plastico = 2
        Ranurado = 3
        Perforado = 4
    End Enum


End Class
