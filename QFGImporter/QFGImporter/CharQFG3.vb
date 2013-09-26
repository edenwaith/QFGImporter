﻿Public Class CharQFG3
    Inherits CharGeneric

#Region "Byte Breakdown"
    ' I don't know the layout of the QFG3/4 files yet, but I suspect they are similar to QFG1/2
    '   except that instead of being 8-bit bytes, it uses 16-bit shorts.
    '
    '
    '
    '
#End Region

    Friend Overrides ReadOnly Property InitialChecksum As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property InitialCipher As Byte
        Get
            Return &H53
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetCharClass As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetChecksum As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetEOF As Byte
        Get
            Return 255
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetExperience As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetInventory As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetOther As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetOther2 As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetSkills As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property OffsetSpells As Byte
        Get
            Return 0
        End Get
    End Property

    Friend Overrides ReadOnly Property SkillMaximum As UShort
        Get
            Return 300
        End Get
    End Property

    Friend Overrides ReadOnly Property SkillTechnicalMaximum As UShort
        Get
            Return 300
        End Get
    End Property

    Public Property EncodedData2 As UShort()
    Public Property DecodedValues2 As UShort()

    Public Sub New(fileContents)
        Call Load(fileContents)
        Me.EncodedData2 = ConvertByteToShort(Me.EncodedData)
        'Me.DecodedValues2 = CharGeneric.DecodeBytesXor(Me.EncodedData2, &H53)
        Me.DecodedValues2 = Me.EncodedData2
    End Sub

    Private Function ConvertByteToShort(bytes As Byte()) As UShort()
        Dim shorts(bytes.Length / 2 - 1) As UShort
        For i As Integer = 0 To shorts.Length - 1
            shorts(i) = (CUShort(bytes(2 * i)) << 8) Or bytes(2 * i + 1)
        Next
        Return shorts
    End Function

    Friend Overrides Sub SetGame()
        Me.Game = Enums.Games.QFG3
    End Sub
End Class