﻿using System.Net;

namespace ServerBase.Response;

internal static class ResponseWriter
{
    public static void WriteStatus(HttpStatusCode code, Stream stream)
    {
        using var writer = new StreamWriter(stream, leaveOpen:true);
        writer.WriteLine($"HTTP/1.0 {(code == HttpStatusCode.NotFound)}");
        writer.WriteLine();
    }
}