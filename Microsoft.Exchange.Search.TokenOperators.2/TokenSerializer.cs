using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.Exchange.Diagnostics.Components.Search;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000026 RID: 38
	internal sealed class TokenSerializer
	{
		// Token: 0x06000128 RID: 296 RVA: 0x0000645D File Offset: 0x0000465D
		internal TokenSerializer()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006468 File Offset: 0x00004668
		internal void Serialize(Stream outputStream, TokenInfo token, bool compress)
		{
			if (compress)
			{
				long position = outputStream.Position;
				long position2;
				using (GZipStream gzipStream = new GZipStream(outputStream, CompressionMode.Compress, true))
				{
					using (BufferedStream bufferedStream = new BufferedStream(gzipStream))
					{
						this.Serialize(bufferedStream, token);
						position2 = bufferedStream.Position;
					}
				}
				ExTraceGlobals.AnnotationTokenTracer.TraceDebug<long, long>((long)this.GetHashCode(), "Serialized token blob is {0} bytes, and compressed to {1} bytes.", position2, outputStream.Position - position);
				return;
			}
			this.Serialize(outputStream, token);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000064FC File Offset: 0x000046FC
		internal TokenInfo Deserialize(Stream inputStream, bool decompress)
		{
			if (decompress)
			{
				using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress, true))
				{
					return this.Deserialize(gzipStream);
				}
			}
			return this.Deserialize(inputStream);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006544 File Offset: 0x00004744
		private void Serialize(Stream serializationStream, TokenInfo token)
		{
			TokenSerializer.TokenBinaryWriter tokenBinaryWriter = new TokenSerializer.TokenBinaryWriter(serializationStream);
			tokenBinaryWriter.Write(token.Text);
			long num = 0L;
			foreach (AnnotationInfo annotationInfo in token.Annotations)
			{
				TokenSerializer.AnnotationInfoType annotationInfoType = this.GetAnnotationInfoType(annotationInfo);
				tokenBinaryWriter.Write((byte)annotationInfoType);
				int value = (int)(annotationInfo.Start - num);
				tokenBinaryWriter.Write(value);
				num = annotationInfo.Start;
				int value2 = (int)(annotationInfo.End - annotationInfo.Start);
				tokenBinaryWriter.Write(value2);
				if ((byte)(annotationInfoType & TokenSerializer.AnnotationInfoType.Attributes) != 0)
				{
					int count = annotationInfo.Attributes.Count;
					tokenBinaryWriter.Write(count);
					foreach (KeyValuePair<string, string> keyValuePair in annotationInfo.Attributes)
					{
						tokenBinaryWriter.Write(keyValuePair.Key);
						tokenBinaryWriter.Write(keyValuePair.Value);
					}
				}
				if ((byte)(annotationInfoType & TokenSerializer.AnnotationInfoType.NumericalAttributes) != 0)
				{
					int count2 = annotationInfo.NumericalAttributes.Count;
					tokenBinaryWriter.Write(count2);
					foreach (KeyValuePair<string, double> keyValuePair2 in annotationInfo.NumericalAttributes)
					{
						tokenBinaryWriter.Write(keyValuePair2.Key);
						tokenBinaryWriter.Write(keyValuePair2.Value);
					}
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000066F0 File Offset: 0x000048F0
		private TokenInfo Deserialize(Stream serializationStream)
		{
			TokenSerializer.TokenBinaryReader tokenBinaryReader = new TokenSerializer.TokenBinaryReader(serializationStream);
			string originalText = tokenBinaryReader.ReadString();
			TokenInfo tokenInfo = new TokenInfo(originalText);
			byte[] array = new byte[1];
			long num = 0L;
			while (tokenBinaryReader.Read(array, 0, 1) != 0)
			{
				byte b = array[0];
				int num2 = tokenBinaryReader.ReadInt32();
				num += (long)num2;
				int length = tokenBinaryReader.ReadInt32();
				AnnotationInfo annotationInfo;
				if ((b & 1) != 0)
				{
					annotationInfo = new AnnotationInfo("alttoken", num, length);
				}
				else
				{
					annotationInfo = new AnnotationInfo("token", num, length);
				}
				if ((b & 2) != 0)
				{
					int num3 = tokenBinaryReader.ReadInt32();
					for (int i = 0; i < num3; i++)
					{
						string key = tokenBinaryReader.ReadString();
						string value = tokenBinaryReader.ReadString();
						annotationInfo.Attributes.Add(new KeyValuePair<string, string>(key, value));
					}
				}
				if ((b & 4) != 0)
				{
					int num4 = tokenBinaryReader.ReadInt32();
					for (int j = 0; j < num4; j++)
					{
						string key2 = tokenBinaryReader.ReadString();
						double value2 = tokenBinaryReader.ReadDouble();
						annotationInfo.NumericalAttributes.Add(new KeyValuePair<string, double>(key2, value2));
					}
				}
				tokenInfo.Annotations.Add(annotationInfo);
			}
			return tokenInfo;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000680C File Offset: 0x00004A0C
		private TokenSerializer.AnnotationInfoType GetAnnotationInfoType(AnnotationInfo annotationInfo)
		{
			TokenSerializer.AnnotationInfoType annotationInfoType = TokenSerializer.AnnotationInfoType.Default;
			if (annotationInfo.Name == "alttoken")
			{
				annotationInfoType |= TokenSerializer.AnnotationInfoType.Alttoken;
			}
			if (annotationInfo.Attributes.Count > 0 && annotationInfo.Attributes[0].Key != "type" && annotationInfo.Attributes[0].Value != "---")
			{
				annotationInfoType |= TokenSerializer.AnnotationInfoType.Attributes;
			}
			if (annotationInfo.NumericalAttributes.Count > 0)
			{
				annotationInfoType |= TokenSerializer.AnnotationInfoType.NumericalAttributes;
			}
			return annotationInfoType;
		}

		// Token: 0x02000027 RID: 39
		[Flags]
		private enum AnnotationInfoType : byte
		{
			// Token: 0x04000085 RID: 133
			Default = 0,
			// Token: 0x04000086 RID: 134
			Alttoken = 1,
			// Token: 0x04000087 RID: 135
			Attributes = 2,
			// Token: 0x04000088 RID: 136
			NumericalAttributes = 4
		}

		// Token: 0x02000028 RID: 40
		private sealed class TokenBinaryWriter : BinaryWriter
		{
			// Token: 0x0600012E RID: 302 RVA: 0x00006899 File Offset: 0x00004A99
			public TokenBinaryWriter(Stream output) : base(output)
			{
			}

			// Token: 0x0600012F RID: 303 RVA: 0x000068A2 File Offset: 0x00004AA2
			public override void Write(int value)
			{
				base.Write7BitEncodedInt(value);
			}
		}

		// Token: 0x02000029 RID: 41
		private sealed class TokenBinaryReader : BinaryReader
		{
			// Token: 0x06000130 RID: 304 RVA: 0x000068AB File Offset: 0x00004AAB
			public TokenBinaryReader(Stream input) : base(input)
			{
			}

			// Token: 0x06000131 RID: 305 RVA: 0x000068B4 File Offset: 0x00004AB4
			public override int ReadInt32()
			{
				return base.Read7BitEncodedInt();
			}
		}
	}
}
