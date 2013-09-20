﻿Public Class Form1
    Private Property Loading As Boolean = True
    Private Property OriginalTitle As String = String.Empty
    Private Property LoadedFilename As String = String.Empty
    Public Property LoadedChar As CharGeneric = Nothing
    Public Property OtherChar As CharGeneric = Nothing
    Public DeltaOffsets As New Collections.ArrayList

    Private Sub btnLoad_Click(sender As System.Object, e As System.EventArgs) Handles btnLoad.Click
        Dim fso As New OpenFileDialog
        fso.Filter = "QFG Import Character (*.sav)|*.sav|All Files (*.*)|*.*"
        If fso.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.LoadedFilename = fso.FileName
            Dim fileContents As String = String.Empty
            Dim s As IO.FileStream = fso.OpenFile()
            If s.Length < 1024 Then
                Dim t As New IO.StreamReader(s)
                fileContents = t.ReadToEnd
            End If
            s.Close()

            'set the form's title, baased on the open file's name
            Me.Text = Me.OriginalTitle & ": " & System.IO.Path.GetFileName(Me.LoadedFilename)

            'CharGeneric.ParseCharacter(fileContents)
            Select Case CharGeneric.GetGame(fileContents)
                Case Enums.Games.QFG1
                    Me.LoadedChar = New CharQFG1(fileContents)
                    Call LoadForm()
                Case Enums.Games.QFG2
                Case Enums.Games.QFG3
                Case Enums.Games.QFG4

            End Select

        End If
    End Sub

    Private Sub LoadForm()
        If Me.LoadedChar Is Nothing Then
            grpAbilities.Enabled = False
            grpClass.Enabled = False
            grpGames.Enabled = False
            grpSkills.Enabled = False
            grpSpells.Enabled = False
        Else
            If TypeOf Me.LoadedChar Is CharQFG1 Then
                rdoQFG1.Checked = True
                SelectClass(Me.LoadedChar.CharacterClass)
                txtName.Text = Me.LoadedChar.Name


                numStrength.Value = Me.LoadedChar.Strength
                numIntelligence.Value = Me.LoadedChar.Intelligence
                numAgility.Value = Me.LoadedChar.Agility
                numVitality.Value = Me.LoadedChar.Vitality
                numLuck.Value = Me.LoadedChar.Luck

                numWeaponUse.Value = Me.LoadedChar.GetSkills(Enums.Skills.WeaponUse)
                numParry.Value = Me.LoadedChar.GetSkills(Enums.Skills.Parry)
                numDodge.Value = Me.LoadedChar.GetSkills(Enums.Skills.Dodge)
                numStealth.Value = Me.LoadedChar.GetSkills(Enums.Skills.Stealth)
                numPickLocks.Value = Me.LoadedChar.GetSkills(Enums.Skills.Picklocks)
                numThrowing.Value = Me.LoadedChar.GetSkills(Enums.Skills.Throwing)
                numClimbing.Value = Me.LoadedChar.GetSkills(Enums.Skills.Climbing)
                numMagic.Value = Me.LoadedChar.GetSkills(Enums.Skills.Magic)

                numOpen.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Open)
                numDetectMagic.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Detect)
                numTrigger.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Trigger)
                numDazzle.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Dazzle)
                numZap.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Zap)
                numCalm.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Calm)
                numFlameDart.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Flame)
                numFetch.Value = Me.LoadedChar.GetMagicSpell(Enums.Magic.Fetch)

                numExperience.Value = Me.LoadedChar.Experience
                numHealthPoints.Value = Me.LoadedChar.HealthPoints
                numStaminaPoints.Value = Me.LoadedChar.StaminaPoints
                numMagicPoints.Value = Me.LoadedChar.MagicPoints
                numPuzzlePoints.Maximum = 500
                numPuzzlePoints.Value = Me.LoadedChar.PuzzlePoints

                'unique inventory
                chkSword.Checked = Me.LoadedChar.HasSword
                chkChainmail.Checked = Me.LoadedChar.HasChainmailArmor
                chkLockPick.Checked = Me.LoadedChar.HasLockPick
                chkToolkit.Checked = Me.LoadedChar.HasThievesToolkit
                chkUnknown1.Checked = Me.LoadedChar.Unknown1
                chkDefeatedBabaYaga.Checked = Me.LoadedChar.Unknown2
                chkUnknown3.Checked = Me.LoadedChar.Unknown3

                'inventory
                numGold.Maximum = 10000
                numGold.Value = Me.LoadedChar.Gold

                numDaggers.Value = Me.LoadedChar.Daggers
                numHealingPotions.Value = Me.LoadedChar.HealingPotions
                numVigorPotions.Value = Me.LoadedChar.StaminaPotions
                numMagicPotions.Value = Me.LoadedChar.MagicPotions
                numUndeadUnguent.Value = DirectCast(Me.LoadedChar, CharQFG1).UndeadUnguent
            End If

        End If
        EnableTestData(True)
        Call LoadTestData()
    End Sub

    Private Sub SelectClass(charClass As Enums.CharacterClass)
        Select Case charClass
            Case Enums.CharacterClass.Fighter
                rdoFighter.Checked = True
            Case Enums.CharacterClass.Magic
                rdoWizard.Checked = True
            Case Enums.CharacterClass.Thief
                rdoThief.Checked = True
            Case Enums.CharacterClass.Paladin
                rdoPaladin.Checked = True
        End Select

    End Sub

    Private Sub EnableQFG1(enabled As Boolean)
        'Abilities QFG1
        lblStrength.Enabled = enabled
        numStrength.Enabled = enabled
        lblIntelligence.Enabled = enabled
        numIntelligence.Enabled = enabled
        lblAgility.Enabled = enabled
        numAgility.Enabled = enabled
        lblVitality.Enabled = enabled
        numVitality.Enabled = enabled
        lblLuck.Enabled = enabled
        numLuck.Enabled = enabled

        'Skills QFG1
        lblWeaponUse.Enabled = enabled
        numWeaponUse.Enabled = enabled
        lblParry.Enabled = enabled
        numParry.Enabled = enabled
        lblDodge.Enabled = enabled
        numDodge.Enabled = enabled
        lblStealth.Enabled = enabled
        numStealth.Enabled = enabled
        lblPickLocks.Enabled = enabled
        numPickLocks.Enabled = enabled
        lblThrowing.Enabled = enabled
        numThrowing.Enabled = enabled
        lblClimbing.Enabled = enabled
        numClimbing.Enabled = enabled
        lblMagic.Enabled = enabled
        numMagic.Enabled = enabled

        'Other QFG1
        lblExperience.Enabled = enabled
        numExperience.Enabled = enabled

        'Magic spells QFG1
        lblOpen.Enabled = enabled
        numOpen.Enabled = enabled
        lblDetectMagic.Enabled = enabled
        numDetectMagic.Enabled = enabled
        lblTrigger.Enabled = enabled
        numTrigger.Enabled = enabled
        lblDazzle.Enabled = enabled
        numDazzle.Enabled = enabled
        lblZap.Enabled = enabled
        numZap.Enabled = enabled
        lblCalm.Enabled = enabled
        numCalm.Enabled = enabled
        lblFlameDart.Enabled = enabled
        numFlameDart.Enabled = enabled
        lblFetch.Enabled = enabled
        numFetch.Enabled = enabled

    End Sub

    Private Sub EnableQFG2(enabled As Boolean)
        'magic spells QFG2
        lblForceBolt.Enabled = enabled
        numForceBolt.Enabled = enabled
        lblLevitation.Enabled = enabled
        numLevitation.Enabled = enabled
        lblReversal.Enabled = enabled
        numReversal.Enabled = enabled

        'skills QFG2
        lblCommunication.Enabled = enabled
        numCommunication.Enabled = enabled

        'other QFG2
        lblHonor.Enabled = enabled
        numHonor.Enabled = enabled

        rdoPaladin.Enabled = enabled
    End Sub

    Private Sub EnableQFG3(enabled As Boolean)
        lblJugglingLights.Enabled = enabled
        numJugglingLights.Enabled = enabled
        lblLightningBall.Enabled = enabled
        numLightningBall.Enabled = enabled
        lblSummonStaff.Enabled = enabled
        numSummonStaff.Enabled = enabled
    End Sub

    Private Sub EnableQFG4(enabled As Boolean)
        lblAcrobatics.Enabled = enabled
        numAcrobatics.Enabled = enabled

        lblGlide.Enabled = enabled
        numGlide.Enabled = enabled
        lblAura.Enabled = enabled
        numAura.Enabled = enabled
        lblFrostBite.Enabled = enabled
        numFrostBite.Enabled = enabled
        lblHide.Enabled = enabled
        numHide.Enabled = enabled
        lblProtection.Enabled = enabled
        numProtection.Enabled = enabled
        lblResistance.Enabled = enabled
        numResistance.Enabled = enabled
    End Sub

    Private Sub Test()
        Dim qfg1 As CharQFG1 = LoadedChar
        Dim output As String = String.Empty
        For Each i As Integer In qfg1.DecodedValues
            output &= i.ToString & " "
        Next
        'MessageBox.Show(output)

        Dim newValues(qfg1.DecodedValues.Length - 1) As Integer
        newValues(0) = qfg1.EncodedData(0) >> 6
        For i As Integer = 1 To newValues.Length - 1
            newValues(i) = qfg1.EncodedData(i) >> 4 Or qfg1.EncodedData(i - 1) << 4
        Next

        Dim output2 As String = String.Empty
        For Each i As Integer In newValues
            output2 &= i.ToString & " "
        Next

        'MessageBox.Show(output & vbCrLf & vbCrLf & output2)
    End Sub

    Private Sub rdoQFG_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoQFG1.CheckedChanged, rdoQFG2.CheckedChanged, rdoQFG3.CheckedChanged, rdoQFG4.CheckedChanged
        Select Case DirectCast(sender, Control).Name
            Case rdoQFG1.Name
            Case rdoQFG2.Name
            Case rdoQFG3.Name
            Case rdoQFG4.Name

        End Select

        EnableQFG4(rdoQFG4.Checked)
        EnableQFG3(rdoQFG4.Checked Or rdoQFG3.Checked)
        EnableQFG2(rdoQFG4.Checked Or rdoQFG3.Checked Or rdoQFG2.Checked)
        EnableQFG1(rdoQFG4.Checked Or rdoQFG3.Checked Or rdoQFG2.Checked Or rdoQFG1.Checked)
        If rdoQFG1.Checked And rdoPaladin.Checked Then
            rdoFighter.Checked = True
        End If
        rdoPaladin.Enabled = True
    End Sub

    Private Sub btnTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTest.Click
        Dim tstDlg As New TestForm

        If tstDlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileContents As String = tstDlg.FileContents

            'CharGeneric.ParseCharacter(fileContents)
            Select Case CharGeneric.GetGame(fileContents)
                Case Enums.Games.QFG1
                    Me.LoadedChar = New CharQFG1(fileContents)
                    Call LoadForm()
                Case Enums.Games.QFG2
                Case Enums.Games.QFG3
                Case Enums.Games.QFG4

            End Select

        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.OriginalTitle = Me.Text
        EnableTestData(False)
        Me.Loading = False
        Call SetMaximumSkill(127)
    End Sub

    Private Sub EnableTestData(enabled As Boolean)
        For Each x As Control In TabPage2.Controls
            x.Enabled = enabled
        Next
    End Sub

    Private Sub LoadTestData()
        txtOriginalData.Text = Me.LoadedChar.DecodedValuesToString
        numOffset.Maximum = Me.LoadedChar.DecodedValues.Length - 1

        Call LoadTestValue()
        Call HighlightData()
    End Sub

    Private Sub LoadTestValue()

        Dim offset As Integer = CInt(numOffset.Value)
        Dim byteCount As Integer = CInt(numBytes.Value)

        numValue.Maximum = 2 ^ (8 * byteCount) - 1
        numValueHex.Maximum = numValue.Maximum
        Dim testvalue As Integer = 0
        testvalue = Me.LoadedChar.DecodedValues(offset)
        If (byteCount > 1) And (offset + byteCount < Me.LoadedChar.DecodedValues.Length) Then
            Dim bytes(byteCount - 1) As Byte
            For i As Integer = 0 To byteCount - 1
                bytes(i) = Me.LoadedChar.DecodedValues(offset + i)
            Next
            If rdoBE.Checked Then
                testvalue = combineBytesBE(bytes)
            Else
                testvalue = combineBytesLE(bytes)
            End If
        End If
        Call SetDisplayValue(testvalue)

        'and load the bit checkboxes for the other ("comparrison") file
        If Me.OtherChar IsNot Nothing Then
            numOtherValue.Maximum = numValue.Maximum
            numOtherValueHex.Maximum = numOtherValue.Maximum
            Dim tstOther As Integer = 0
            tstOther = Me.OtherChar.DecodedValues(offset)

            If (byteCount > 1) And (offset + byteCount < Me.OtherChar.DecodedValues.Length) Then
                Dim bytes(byteCount - 1) As Byte
                For i As Integer = 0 To byteCount - 1
                    bytes(i) = Me.OtherChar.DecodedValues(offset + i)
                Next
                If rdoBE.Checked Then
                    tstOther = combineBytesBE(bytes)
                Else
                    tstOther = combineBytesLE(bytes)
                End If
            End If


            Call SetDisplayValueOther(tstOther)

        End If


        lblByteName.Text = DirectCast(CInt(numOffset.Value), CharQFG1.ByteNames).ToString
    End Sub

    Private Sub SetDisplayValue(value As Integer)
        numValue.Value = value
        numValueHex.Value = value

        If value < 256 Then
            SetBitCheckbox(value)
        Else
            SetBitCheckbox(0)
        End If

    End Sub

    Private Sub SetDisplayValueOther(value As Integer)
        numOtherValue.Value = value
        numOtherValueHex.Value = value

        If value < 256 Then
            SetBitCheckboxOther(value)
        Else
            SetBitCheckboxOther(0)
        End If

    End Sub

    Private Function getBit(inputByte As Byte, bit As Byte) As Boolean
        If bit > 7 Then
            Return 0
        End If
        Dim mask As Byte = 1 << bit

        Return inputByte And mask
    End Function

    Private Sub SetBitCheckbox(value As Byte)
        chkBit0.Checked = getBit(value, 0)
        chkBit1.Checked = getBit(value, 1)
        chkBit2.Checked = getBit(value, 2)
        chkBit3.Checked = getBit(value, 3)
        chkBit4.Checked = getBit(value, 4)
        chkBit5.Checked = getBit(value, 5)
        chkBit6.Checked = getBit(value, 6)
        chkBit7.Checked = getBit(value, 7)

    End Sub

    Private Sub SetBitCheckboxOther(value As Byte)
        chkOther0.Checked = getBit(value, 0)
        chkOther1.Checked = getBit(value, 1)
        chkOther2.Checked = getBit(value, 2)
        chkOther3.Checked = getBit(value, 3)
        chkOther4.Checked = getBit(value, 4)
        chkOther5.Checked = getBit(value, 5)
        chkOther6.Checked = getBit(value, 6)
        chkOther7.Checked = getBit(value, 7)
    End Sub

    Private Function combineBytesLE(byteArray As Byte()) As Integer
        Dim rtn As Integer = 0
        Dim len As Integer = byteArray.Length
        Select Case byteArray.Length
            Case 1
                Return byteArray(0)
            Case 2
                Return BitConverter.ToInt16(byteArray, 0)
            Case 3
                Dim newB(4) As Byte
                newB(0) = 0
                For i As Integer = 1 To 3
                    newB(i) = byteArray(i)
                Next
                Return BitConverter.ToInt32(newB, 0)
            Case 4
                Return BitConverter.ToInt32(byteArray, 0)
            Case Else
                Return 0
        End Select
    End Function

    Private Function combineBytesBE(byteArray As Byte()) As Integer
        Dim rtn As Integer = 0
        Dim len As Integer = byteArray.Length
        Array.Reverse(byteArray)
        Select Case byteArray.Length
            Case 1
                Return byteArray(0)
            Case 2
                Return BitConverter.ToInt16(byteArray, 0)
            Case 3
                Dim newB(4) As Byte
                newB(0) = 0
                For i As Integer = 1 To 3
                    newB(i) = byteArray(i - 1)
                Next
                Return BitConverter.ToInt32(newB, 0)
            Case 4
                Return BitConverter.ToInt32(byteArray, 0)
            Case Else
                Return 0
        End Select
    End Function

    Private Sub numOffset_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numOffset.ValueChanged
        Call LoadTestValue()
        Call HighlightData()
    End Sub

    Private Sub HighlightData()
        txtOriginalData.SelectionStart = 3 * numOffset.Value
        txtOriginalData.SelectionLength = (3 * numBytes.Value) - 1
        If Me.OtherChar IsNot Nothing Then
            txtOtherData.SelectionStart = txtOriginalData.SelectionStart
            txtOtherData.SelectionLength = txtOriginalData.SelectionLength
        End If
    End Sub

    Private Sub numBytes_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numBytes.ValueChanged
        If Not Me.Loading Then
            Call HighlightData()
            Call LoadTestValue()
        End If
    End Sub

    Private Sub rdoBE_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoBE.CheckedChanged, rdoLE.CheckedChanged
        If Not Me.Loading Then
            Call LoadTestValue()
        End If
    End Sub

    Private Sub btnLoadOther_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadOther.Click
        Dim fso As New OpenFileDialog
        fso.Filter = "QFG Import Character (*.sav)|*.sav|All Files (*.*)|*.*"
        If fso.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileContents As String = String.Empty
            Dim s As IO.FileStream = fso.OpenFile()
            If s.Length < 1024 Then
                Dim t As New IO.StreamReader(s)
                fileContents = t.ReadToEnd
            End If
            s.Close()

            lblOtherDataFilename.Text = System.IO.Path.GetFileName(fso.FileName)
            'CharGeneric.ParseCharacter(fileContents)
            Select Case CharGeneric.GetGame(fileContents)
                Case Enums.Games.QFG1
                    Me.OtherChar = New CharQFG1(fileContents)
                    Call LoadFormOther()
                Case Enums.Games.QFG2
                Case Enums.Games.QFG3
                Case Enums.Games.QFG4

            End Select

        End If
    End Sub

    Private Sub LoadFormOther()
        If Me.OtherChar IsNot Nothing Then
            numOffset.Value = 0
            txtOtherData.Text = Me.OtherChar.DecodedValuesToString
            Me.DeltaOffsets.Clear()
            For i As Integer = 0 To Me.LoadedChar.DecodedValues.Length - 1
                If Not Me.LoadedChar.DecodedValues(i).Equals(Me.OtherChar.DecodedValues(i)) Then
                    Me.DeltaOffsets.Add(i)
                End If
            Next
            lblDifferences.Text = Me.DeltaOffsets.Count & " Differences"
            SetBitCheckboxOther(Me.OtherChar.DecodedValues(0))
            Call HighlightData()
        Else
            txtOtherData.Text = String.Empty
            lblOtherDataFilename.Text = String.Empty
        End If
    End Sub

    Private Sub btnDeltaPrev_Click(sender As System.Object, e As System.EventArgs) Handles btnDeltaPrev.Click
        For i As Integer = Me.DeltaOffsets.Count - 1 To 0 Step -1
            If Me.DeltaOffsets(i) < numOffset.Value Then
                numOffset.Value = Me.DeltaOffsets(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub btnDeltaNext_Click(sender As System.Object, e As System.EventArgs) Handles btnDeltaNext.Click
        For Each delta As Integer In Me.DeltaOffsets
            If delta > numOffset.Value Then
                numOffset.Value = delta
                Exit For
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim fso As New SaveFileDialog
        fso.Filter = "QFG Import Character (*.sav)|*.sav|All Files (*.*)|*.*"
        fso.InitialDirectory = IO.Path.GetDirectoryName(Me.LoadedFilename)
        fso.FileName = IO.Path.GetFileName(Me.LoadedFilename)
        fso.OverwritePrompt = True
        fso.DefaultExt = ".sav"
        fso.Title = "Save QFG Character File As..."
        If fso.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fs As New IO.StreamWriter(fso.OpenFile())
            fs.Write(Me.LoadedChar.ToByteString)
            fs.Close()
        End If
    End Sub

    Private Sub numAbilitySkill_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numStrength.ValueChanged, numIntelligence.ValueChanged, numAgility.ValueChanged, numVitality.ValueChanged, numLuck.ValueChanged, numWeaponUse.ValueChanged, numParry.ValueChanged, numDodge.ValueChanged, numStealth.ValueChanged, numPickLocks.ValueChanged, numThrowing.ValueChanged, numClimbing.ValueChanged, numMagic.ValueChanged, numCommunication.ValueChanged, numHonor.ValueChanged, numAcrobatics.ValueChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim numValue As NumericUpDown = sender
            Select Case numValue.Name
                Case numStrength.Name
                    Me.LoadedChar.Strength = numValue.Value
                Case numIntelligence.Name
                    Me.LoadedChar.Intelligence = numValue.Value
                Case numAgility.Name
                    Me.LoadedChar.Agility = numValue.Value
                Case numVitality.Name
                    Me.LoadedChar.Vitality = numValue.Value
                Case numLuck.Name
                    Me.LoadedChar.Luck = numValue.Value
                Case numWeaponUse.Name
                    Me.LoadedChar.WeaponUse = numValue.Value
                Case numParry.Name
                    Me.LoadedChar.Parry = numValue.Value
                Case numDodge.Name
                    Me.LoadedChar.Dodge = numValue.Value
                Case numStealth.Name
                    Me.LoadedChar.Stealth = numValue.Value
                Case numPickLocks.Name
                    Me.LoadedChar.PickLocks = numValue.Value
                Case numThrowing.Name
                    Me.LoadedChar.Throwing = numValue.Value
                Case numClimbing.Name
                    Me.LoadedChar.Climbing = numValue.Value
                Case numMagic.Name
                    Me.LoadedChar.Magic = numValue.Value
                Case numCommunication.Name
                    If Me.LoadedChar.Game >= Enums.Games.QFG2 Then
                        'TODO: Add communication change
                    End If
                Case numHonor.Name
                    If Me.LoadedChar.Game >= Enums.Games.QFG2 Then
                        'TODO: Add Honor change
                    End If
                Case numAcrobatics.Name
                    If Me.LoadedChar.Game >= Enums.Games.QFG4 Then
                        'TODO: Add Acrobatics change
                    End If
            End Select
        End If
    End Sub

    Private Sub numMagicSpells_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numOpen.ValueChanged, numDetectMagic.ValueChanged, numTrigger.ValueChanged, numDazzle.ValueChanged, numZap.ValueChanged, numCalm.ValueChanged, numFlameDart.ValueChanged, numFetch.ValueChanged, numForceBolt.ValueChanged, numLevitation.ValueChanged, numReversal.ValueChanged, numJugglingLights.ValueChanged, numLightningBall.ValueChanged, numSummonStaff.ValueChanged, numHide.ValueChanged, numProtection.ValueChanged, numAura.ValueChanged, numGlide.ValueChanged, numResistance.ValueChanged, numFrostBite.ValueChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim numValue As NumericUpDown = sender
            Select Case numValue.Name
                Case numOpen.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Open, numValue.Value)
                Case numDetectMagic.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Detect, numValue.Value)
                Case numTrigger.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Trigger, numValue.Value)
                Case numDazzle.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Dazzle, numValue.Value)
                Case numZap.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Zap, numValue.Value)
                Case numCalm.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Calm, numValue.Value)
                Case numFlameDart.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Flame, numValue.Value)
                Case numFetch.Name
                    Me.LoadedChar.SetMagicSpell(Enums.Magic.Fetch, numValue.Value)
            End Select
        End If
    End Sub

    Private Sub numOther_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numPuzzlePoints.ValueChanged, numExperience.ValueChanged, numHealthPoints.ValueChanged, numStaminaPoints.ValueChanged, numMagicPoints.ValueChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim numValue As NumericUpDown = sender
            Select Case numValue.Name
                Case numPuzzlePoints.Name
                    Me.LoadedChar.PuzzlePoints = numValue.Value
                Case numExperience.Name
                    Me.LoadedChar.Experience = numValue.Value
                Case numHealthPoints.Name
                    Me.LoadedChar.HealthPoints = numValue.Value
                Case numStaminaPoints.Name
                    Me.LoadedChar.StaminaPoints = numValue.Value
                Case numMagicPoints.Name
                    Me.LoadedChar.MagicPoints = numValue.Value
            End Select
        End If
    End Sub

    Private Sub SetMaximumSkill(value As UShort)
        For Each num As Control In grpAbilities.Controls
            If TypeOf num Is NumericUpDown Then
                DirectCast(num, NumericUpDown).Maximum = value
            End If
        Next

        For Each num As Control In grpSkills.Controls
            If TypeOf num Is NumericUpDown Then
                DirectCast(num, NumericUpDown).Maximum = value
            End If
        Next

        For Each num As Control In grpSpells.Controls
            If TypeOf num Is NumericUpDown Then
                DirectCast(num, NumericUpDown).Maximum = value
            End If
        Next

        numHealingPotions.Maximum = value
        numStaminaPoints.Maximum = value
        numMagicPoints.Maximum = value

    End Sub

    Private Sub SetMaximumInventory(value As UShort)
        numDaggers.Maximum = value
        numHealingPotions.Maximum = value
        numVigorPotions.Maximum = value
        numMagicPotions.Maximum = value
        numUndeadUnguent.Maximum = value
    End Sub

    Private Sub numInventory_ValueChanged(sender As System.Object, e As System.EventArgs) Handles numGold.ValueChanged, numDaggers.ValueChanged, numHealingPotions.ValueChanged, numVigorPotions.ValueChanged, numMagicPotions.ValueChanged, numUndeadUnguent.ValueChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim numValue As NumericUpDown = sender
            Select Case numValue.Name
                Case numGold.Name
                    Me.LoadedChar.Gold = numValue.Value
                Case numDaggers.Name
                    Me.LoadedChar.Daggers = numValue.Value
                Case numHealingPotions.Name
                    Me.LoadedChar.HealingPotions = numValue.Value
                Case numVigorPotions.Name
                    Me.LoadedChar.StaminaPotions = numValue.Value
                Case numMagicPotions.Name
                    Me.LoadedChar.MagicPotions = numValue.Value
                Case numUndeadUnguent.Name
                    If Me.LoadedChar.Game = Enums.Games.QFG1 Then
                        DirectCast(Me.LoadedChar, CharQFG1).UndeadUnguent = numValue.Value
                    End If
            End Select
        End If
    End Sub

    Private Sub txtName_LostFocus(sender As Object, e As System.EventArgs) Handles txtName.LostFocus
        If Me.LoadedChar IsNot Nothing Then
            DirectCast(sender, TextBox).Text = Me.LoadedChar.Name
        End If
    End Sub

    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged
        If Me.LoadedChar IsNot Nothing Then
            Me.LoadedChar.Name = DirectCast(sender, TextBox).Text
        End If
    End Sub

    Private Sub chkUniqueInventory_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSword.CheckedChanged, chkChainmail.CheckedChanged, chkLockPick.CheckedChanged, chkToolkit.CheckedChanged, chkUnknown1.CheckedChanged, chkDefeatedBabaYaga.CheckedChanged, chkUnknown3.CheckedChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim QFG1 As CharQFG1 = Me.LoadedChar
            Dim chkState As CheckBox = sender
            Select Case chkState.Name
                Case chkSword.Name
                    QFG1.HasSword = chkState.Checked
                Case chkChainmail.Name
                    QFG1.HasChainmailArmor = chkState.Checked
                Case chkLockPick.Name
                    QFG1.HasLockPick = chkState.Checked
                Case chkToolkit.Name
                    QFG1.HasThievesToolkit = chkState.Checked
                Case chkUnknown1.Name
                    QFG1.Unknown1 = chkState.Checked
                Case chkDefeatedBabaYaga.Name
                    QFG1.Unknown2 = chkState.Checked
                Case chkUnknown3.Name
                    QFG1.Unknown3 = chkState.Checked
            End Select
        End If
    End Sub

    Private Sub rdoCharClass_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdoFighter.CheckedChanged, rdoWizard.CheckedChanged, rdoThief.CheckedChanged, rdoPaladin.CheckedChanged
        If Me.LoadedChar IsNot Nothing Then
            Dim classChange As RadioButton = sender
            Select Case classChange.Name
                Case rdoFighter.Name
                    Me.LoadedChar.CharacterClass = Enums.CharacterClass.Fighter
                Case rdoWizard.Name
                    Me.LoadedChar.CharacterClass = Enums.CharacterClass.Magic
                Case rdoThief.Name
                    Me.LoadedChar.CharacterClass = Enums.CharacterClass.Thief
                Case rdoPaladin.Name
                    Me.LoadedChar.CharacterClass = Enums.CharacterClass.Paladin
            End Select
        End If
    End Sub
End Class
