﻿Public Class RssViewerDialog
    Private _feedItem As RSSItem

    Public Sub New(ByVal FeedItem As RSSItem)
        InitializeComponent()

    End Sub
    Private Sub ChannelPassDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       ThemeHandler.ApplyTheme(Me)
    End Sub
End Class