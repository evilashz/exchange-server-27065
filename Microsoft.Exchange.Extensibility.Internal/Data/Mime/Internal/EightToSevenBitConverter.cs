using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000003 RID: 3
	internal static class EightToSevenBitConverter
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000229C File Offset: 0x0000049C
		public static void Convert(Stream source, Stream destination)
		{
			int i = 0;
			byte[][] array = null;
			byte[] array2 = null;
			using (Stream stream = new SuppressCloseStream(source))
			{
				using (MimeReader mimeReader = new MimeReader(stream, true, DecodingOptions.Default, MimeLimits.Unlimited, true, false))
				{
					while (mimeReader.ReadNextPart())
					{
						while (i >= mimeReader.Depth)
						{
							byte[] array3 = array[--i];
							if (array3 != null)
							{
								destination.Write(array3, 0, array3.Length - 2);
								destination.Write(MimeString.TwoDashesCRLF, 0, MimeString.TwoDashesCRLF.Length);
							}
						}
						if (i > 0)
						{
							byte[] array3 = array[i - 1];
							if (array3 != null)
							{
								destination.Write(array3, 0, array3.Length);
							}
						}
						HeaderList headerList = HeaderList.ReadFrom(mimeReader);
						ContentTypeHeader contentTypeHeader = headerList.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
						bool flag;
						bool flag2;
						bool flag3;
						EightToSevenBitConverter.Analyse(contentTypeHeader, out flag, out flag2, out flag3);
						Header header = headerList.FindFirst(HeaderId.ContentTransferEncoding);
						if (flag2 || flag)
						{
							if (header != null)
							{
								headerList.RemoveChild(header);
							}
							headerList.WriteTo(destination);
							byte[] array3;
							if (flag)
							{
								array3 = null;
								destination.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
							}
							else
							{
								array3 = MimePart.GetBoundary(contentTypeHeader);
							}
							if (array == null)
							{
								array = new byte[4][];
							}
							else if (array.Length == i)
							{
								byte[][] array4 = new byte[array.Length * 2][];
								Array.Copy(array, 0, array4, 0, i);
								array = array4;
							}
							array[i++] = array3;
						}
						else
						{
							Stream stream2 = null;
							try
							{
								stream2 = mimeReader.GetRawContentReadStream();
								if (header != null && stream2 != null)
								{
									ContentTransferEncoding encodingType = MimePart.GetEncodingType(header.FirstRawToken);
									if (encodingType == ContentTransferEncoding.EightBit || encodingType == ContentTransferEncoding.Binary)
									{
										if (flag3)
										{
											header.RawValue = MimeString.QuotedPrintable;
											stream2 = new EncoderStream(stream2, new QPEncoder(), EncoderStreamAccess.Read);
										}
										else
										{
											header.RawValue = MimeString.Base64;
											stream2 = new EncoderStream(stream2, new Base64Encoder(), EncoderStreamAccess.Read);
										}
									}
								}
								headerList.WriteTo(destination);
								destination.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
								if (stream2 != null)
								{
									DataStorage.CopyStreamToStream(stream2, destination, long.MaxValue, ref array2);
								}
							}
							finally
							{
								if (stream2 != null)
								{
									stream2.Dispose();
								}
							}
						}
					}
					while (i > 0)
					{
						byte[] array3 = array[--i];
						if (array3 != null)
						{
							destination.Write(array3, 0, array3.Length - 2);
							destination.Write(MimeString.TwoDashesCRLF, 0, MimeString.TwoDashesCRLF.Length);
						}
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000251C File Offset: 0x0000071C
		private static void Analyse(ContentTypeHeader typeHeader, out bool embedded, out bool multipart, out bool useQP)
		{
			embedded = false;
			multipart = false;
			useQP = true;
			if (typeHeader != null)
			{
				multipart = typeHeader.IsMultipart;
				if (!multipart)
				{
					embedded = typeHeader.IsEmbeddedMessage;
					if (!embedded)
					{
						useQP = (typeHeader.MediaType == "text");
					}
				}
			}
		}

		// Token: 0x04000002 RID: 2
		private static readonly byte[] TextSlash = ByteString.StringToBytes("text/", true);

		// Token: 0x04000003 RID: 3
		private static readonly byte[] CteBase64 = ByteString.StringToBytes("Content-Transfer-Encoding: base64\r\n", true);

		// Token: 0x04000004 RID: 4
		private static readonly byte[] CteQP = ByteString.StringToBytes("Content-Transfer-Encoding: quoted-printable\r\n", true);

		// Token: 0x02000004 RID: 4
		internal sealed class OutputFilter : MimeOutputFilter, IDisposable
		{
			// Token: 0x06000007 RID: 7 RVA: 0x00002588 File Offset: 0x00000788
			public override bool FilterHeaderList(HeaderList headerList, Stream stream)
			{
				Header header = headerList.FindFirst(HeaderId.ContentTransferEncoding);
				if (header != null)
				{
					ContentTransferEncoding encodingType = MimePart.GetEncodingType(header.FirstRawToken);
					if (encodingType == ContentTransferEncoding.EightBit || encodingType == ContentTransferEncoding.Binary)
					{
						ContentTypeHeader typeHeader = headerList.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
						bool flag;
						bool flag2;
						EightToSevenBitConverter.Analyse(typeHeader, out this.embedded, out flag, out flag2);
						for (MimeNode mimeNode = headerList.FirstChild; mimeNode != null; mimeNode = mimeNode.NextSibling)
						{
							if (HeaderId.ContentTransferEncoding == (mimeNode as Header).HeaderId)
							{
								if (flag || this.embedded)
								{
									this.encoder = null;
								}
								else if (flag2)
								{
									stream.Write(EightToSevenBitConverter.CteQP, 0, EightToSevenBitConverter.CteQP.Length);
									this.encoder = new QPEncoder();
								}
								else
								{
									stream.Write(EightToSevenBitConverter.CteBase64, 0, EightToSevenBitConverter.CteBase64.Length);
									this.encoder = new Base64Encoder();
								}
							}
							else
							{
								mimeNode.WriteTo(stream);
							}
						}
						stream.Write(MimeString.CrLf, 0, MimeString.CrLf.Length);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002678 File Offset: 0x00000878
			public override bool FilterPartBody(MimePart part, Stream stream)
			{
				if (this.embedded)
				{
					EightToSevenBitConverter.Convert(part.GetRawContentReadStream(), stream);
					this.embedded = false;
					return true;
				}
				if (this.encoder != null)
				{
					using (Stream stream2 = new EncoderStream(part.GetRawContentReadStream(), this.encoder, EncoderStreamAccess.Read))
					{
						DataStorage.CopyStreamToStream(stream2, stream, long.MaxValue, ref this.scratchBuffer);
						this.encoder = null;
						return true;
					}
					return false;
				}
				return false;
			}

			// Token: 0x06000009 RID: 9 RVA: 0x000026FC File Offset: 0x000008FC
			public void Dispose()
			{
				if (this.encoder != null)
				{
					this.encoder.Dispose();
					this.encoder = null;
				}
			}

			// Token: 0x04000005 RID: 5
			private ByteEncoder encoder;

			// Token: 0x04000006 RID: 6
			private bool embedded;

			// Token: 0x04000007 RID: 7
			private byte[] scratchBuffer;
		}
	}
}
