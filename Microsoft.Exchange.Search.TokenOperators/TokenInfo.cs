using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.Search;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000025 RID: 37
	internal sealed class TokenInfo
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000062A8 File Offset: 0x000044A8
		internal TokenInfo(string originalText)
		{
			this.annotations = new List<AnnotationInfo>();
			this.fieldText = originalText;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000062C2 File Offset: 0x000044C2
		internal ICollection<AnnotationInfo> Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000062CA File Offset: 0x000044CA
		internal string Text
		{
			get
			{
				return this.fieldText;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000062D4 File Offset: 0x000044D4
		internal static TokenInfo Create(Stream inputStream)
		{
			ExTraceGlobals.AnnotationTokenTracer.TraceDebug<int>((long)inputStream.GetHashCode(), "Check {0} bytes for the version data.", TokenInfo.Version.Length);
			byte[] array = new byte[TokenInfo.Version.Length];
			int num = inputStream.Read(array, 0, TokenInfo.Version.Length);
			if (num != TokenInfo.Version.Length)
			{
				throw new ArgumentException("inputStream");
			}
			if (!TokenInfo.IsVersionSupported(array))
			{
				throw new NotSupportedException("Token version not supported");
			}
			ExTraceGlobals.AnnotationTokenTracer.TraceDebug<long>((long)inputStream.GetHashCode(), "Deserializing the token blob of {0} bytes.", inputStream.Length - (long)TokenInfo.Version.Length);
			return TokenInfo.formatter.Deserialize(inputStream, true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006374 File Offset: 0x00004574
		internal static bool IsVersionSupported(byte[] blob)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (blob.Length < TokenInfo.Version.Length)
			{
				return false;
			}
			for (int i = 0; i < TokenInfo.Version.Length; i++)
			{
				if (TokenInfo.Version[i] != blob[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000063C0 File Offset: 0x000045C0
		internal void SerializeTo(Stream outputStream)
		{
			ExTraceGlobals.AnnotationTokenTracer.TraceDebug<int>((long)this.GetHashCode(), "Writing version data of {0} bytes.", TokenInfo.Version.Length);
			outputStream.Write(TokenInfo.Version, 0, TokenInfo.Version.Length);
			TokenInfo.formatter.Serialize(outputStream, this, true);
			ExTraceGlobals.AnnotationTokenTracer.TraceDebug<long>((long)this.GetHashCode(), "Serialized token blob is {0} bytes.", outputStream.Position - (long)TokenInfo.Version.Length);
		}

		// Token: 0x04000080 RID: 128
		internal static readonly byte[] Version = new byte[]
		{
			15,
			0,
			0,
			3
		};

		// Token: 0x04000081 RID: 129
		private static readonly TokenSerializer formatter = new TokenSerializer();

		// Token: 0x04000082 RID: 130
		private readonly List<AnnotationInfo> annotations;

		// Token: 0x04000083 RID: 131
		private readonly string fieldText;
	}
}
