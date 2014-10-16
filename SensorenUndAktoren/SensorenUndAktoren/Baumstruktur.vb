Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class Baumstruktur

   

    Private Class Datensatz
        Private _Schluessel As String
        Public Property Schluessel() As String
            Get
                Return _Schluessel
            End Get
            Set(ByVal value As String)
                _Schluessel = value
            End Set
        End Property

        Private _Text As String
        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        Private _Koppelung As String
        Public Property Koppelung() As String
            Get
                Return _Koppelung
            End Get
            Set(ByVal value As String)
                _Koppelung = value
            End Set
        End Property

        'Element wurde in Baumstruktur eingebaut
        Private _InBaumstruktur As Boolean
        Public Property InBaumstruktur() As Boolean
            Get
                Return _InBaumstruktur
            End Get
            Set(ByVal value As Boolean)
                _InBaumstruktur = value
            End Set
        End Property
    End Class


    Private DatenTabelle As New List(Of Datensatz)

    
    Private _KnotenListe As TreeNodeCollection
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns>TreeNodeCollection</returns>
    ''' <remarks></remarks>
    Public Property KnotenListe() As TreeNodeCollection
        Get
            Return _KnotenListe
        End Get
        Set(ByVal value As TreeNodeCollection)
            _KnotenListe = value
        End Set
    End Property



    Public Function DatensatzHinzufügen(Schluessel As String, Text As String, Koppelung As String) As Boolean

        Dim Daten As New Datensatz

        Daten.Schluessel = Schluessel
        Daten.Text = Text
        Daten.Koppelung = Koppelung
        Daten.InBaumStruktur = False
        DatenTabelle.Add(Daten)
    End Function

    Public Sub StrukturAufbauen(ByRef Knoten As TreeNode, Optional ByVal Tiefenindex As Long = 0)


        For Each item In DatenTabelle 'erster Durchlauf

            If item.Koppelung = "" And item.InBaumstruktur = False And Tiefenindex = 0 Then

                item.InBaumstruktur = True
                StrukturAufbauen(Knoten.Nodes.Add(item.Schluessel, item.Text), Tiefenindex + 1)


            End If
        Next



        For Each item As Datensatz In DatenTabelle

            If Knoten.Name = item.Koppelung And item.InBaumstruktur = False Then

                item.InBaumstruktur = True
                StrukturAufbauen(Knoten.Nodes.Add(item.Schluessel, item.Text), Tiefenindex + 1)


            End If


        Next


    End Sub


End Class
