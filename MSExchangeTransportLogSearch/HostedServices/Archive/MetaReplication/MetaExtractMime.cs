using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000048 RID: 72
	public class MetaExtractMime : IDisposable
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000B0EC File Offset: 0x000092EC
		public MetaExtractMime(string from, Stream outputStream)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.mimeWriter = new StreamWriter(outputStream);
			this.mimeWriter.AutoFlush = true;
			this.headers.Add(new KeyValuePair<string, string>("From", from));
			this.headers.Add(new KeyValuePair<string, string>("Date", new DateHeader("Date", DateTime.UtcNow).Value));
			this.headers.Add(new KeyValuePair<string, string>("MIME-Version", "1.0"));
			this.headers.Add(new KeyValuePair<string, string>("Content-Type", "multipart/mixed; boundary=\"blob-boundary\""));
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000B1C3 File Offset: 0x000093C3
		public Stream MimeStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000B1CC File Offset: 0x000093CC
		public static XmlWriterSettings CreateXmlWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = false,
				CloseOutput = false,
				Encoding = Encoding.UTF8,
				CheckCharacters = false,
				OmitXmlDeclaration = false,
				NewLineHandling = NewLineHandling.Entitize
			};
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B20E File Offset: 0x0000940E
		private static void CheckIsValidExtendedHeader(string header)
		{
			if (!header.StartsWith("X-", StringComparison.Ordinal))
			{
				throw new ArgumentException("Header is not a valid extended header: " + header);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000B230 File Offset: 0x00009430
		public void AddExtendedHeaderValue(string header, string value)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			MetaExtractMime.CheckIsValidExtendedHeader(header);
			if (this.blobPartCount == 0)
			{
				this.extendedHeaders.Add(new KeyValuePair<string, string>(header, value));
				return;
			}
			throw new InvalidOperationException("Cannot add extended headers after adding blob content");
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000B284 File Offset: 0x00009484
		public void AddExtendedHeaderValues(string header, IEnumerable<string> values)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			MetaExtractMime.CheckIsValidExtendedHeader(header);
			if (this.blobPartCount == 0)
			{
				using (IEnumerator<string> enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (text == null)
						{
							throw new ArgumentException("values cannot contain null values", "values");
						}
						this.extendedHeaders.Add(new KeyValuePair<string, string>(header, text));
					}
					return;
				}
			}
			throw new InvalidOperationException("Cannot add extended headers after adding blob content");
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000B320 File Offset: 0x00009520
		public void SetContentDescriptionsProvider(MimeContentDescriptions descriptions)
		{
			if (descriptions == null)
			{
				throw new ArgumentNullException("descriptions");
			}
			this.contentDescriptions = descriptions;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B337 File Offset: 0x00009537
		private void AddBlobPart(object item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.AddBlobPartInternal(item, false, null, string.Empty, string.Empty);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B35A File Offset: 0x0000955A
		private void AddBlobPart(object item, string contentDescription)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (contentDescription == null)
			{
				throw new ArgumentNullException("contentDescription");
			}
			this.AddBlobPartInternal(item, false, contentDescription, string.Empty, string.Empty);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B38C File Offset: 0x0000958C
		public bool AddBlobPart(object item, bool computeContentMd5, string ehaMessageId, string eoaMessageStatus)
		{
			if (item != null)
			{
				try
				{
					this.AddBlobPartInternal(item, computeContentMd5, null, ehaMessageId, eoaMessageStatus);
					return true;
				}
				catch (InvalidOperationException ex)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MetaExtractMime AddBlobPart exception " + ex.ToString());
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B3E4 File Offset: 0x000095E4
		private void AddBlobPart(object item, bool computeContentMd5, string contentDescription)
		{
			if (contentDescription == null)
			{
				throw new ArgumentNullException("contentDescription");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.AddBlobPartInternal(item, computeContentMd5, contentDescription, string.Empty, string.Empty);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B418 File Offset: 0x00009618
		public void InsertLastPartEndingBoundary()
		{
			if (!this.lastPartEndingBoundaryInserted && this.mimeWriter != null && this.blobPartCount > 0)
			{
				this.mimeWriter.WriteLine("--");
				this.mimeWriter.WriteLine();
				this.mimeWriter.Flush();
				this.lastPartEndingBoundaryInserted = true;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B46B File Offset: 0x0000966B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B474 File Offset: 0x00009674
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mimeWriter != null && this.outputStream != null)
				{
					this.InsertLastPartEndingBoundary();
				}
				if (this.mimeWriter != null)
				{
					this.mimeWriter.Dispose();
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B4A2 File Offset: 0x000096A2
		private void WriteHeader(string name, string value)
		{
			this.mimeWriter.WriteLine("{0}: {1}", name, value);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B4B8 File Offset: 0x000096B8
		private void AddBlobPartInternal(object item, bool computeContentMd5, string contentDescription, string ehaMessageId, string eoaMessageStatus)
		{
			if (contentDescription == null)
			{
				if (this.contentDescriptions == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Content description is not known for type '{0}'.  Either provide the content description, or call SetContentDescriptionsProvider to set the provider which provides content descriptions for types.", new object[]
					{
						item.GetType()
					}));
				}
				contentDescription = this.contentDescriptions.GetContentDescription(item.GetType());
				if (contentDescription == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Content description for type '{0}' could not be found.  It is either not registered for MIME content serialization or does not have the MimeContentAttribute attribute applied to it.", new object[]
					{
						item.GetType()
					}));
				}
			}
			byte[] array = null;
			string value;
			this.SerializeItem(item, computeContentMd5, out value, out array);
			if (this.blobPartCount > 0)
			{
				this.mimeWriter.WriteLine();
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.extendedHeaders)
				{
					this.WriteHeader(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in this.headers)
				{
					this.WriteHeader(keyValuePair2.Key, keyValuePair2.Value);
				}
				this.mimeWriter.WriteLine();
				this.mimeWriter.WriteLine("This is a multipart message in MIME format");
				this.mimeWriter.WriteLine();
				this.mimeWriter.WriteLine("--blob-boundary");
			}
			this.WriteHeader("Content-Type", "text/xml; charset=utf-8");
			this.WriteHeader("Content-Transfer-Encoding", "base64");
			this.WriteHeader("Content-Description", contentDescription);
			this.WriteHeader("X-MS-EHAMessageID", ehaMessageId);
			this.WriteHeader("X-MS-EOAStatus", eoaMessageStatus);
			if (array != null)
			{
				this.WriteHeader("Content-MD5", Convert.ToBase64String(array));
			}
			this.mimeWriter.WriteLine();
			this.mimeWriter.Write(value);
			this.mimeWriter.WriteLine();
			this.mimeWriter.Write("--blob-boundary");
			this.blobPartCount++;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B6D4 File Offset: 0x000098D4
		private void SerializeItem(object item, bool computeContentMd5, out string contentString, out byte[] computedMd5)
		{
			computedMd5 = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, MetaExtractMime.CreateXmlWriterSettings()))
				{
					XmlSerializationHelper.Serialize(item, xmlWriter);
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				contentString = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.InsertLineBreaks);
				if (computeContentMd5)
				{
					computedMd5 = null;
				}
			}
		}

		// Token: 0x0400014A RID: 330
		private const string MimeBoundary = "blob-boundary";

		// Token: 0x0400014B RID: 331
		private StreamWriter mimeWriter;

		// Token: 0x0400014C RID: 332
		private Stream outputStream;

		// Token: 0x0400014D RID: 333
		private int blobPartCount;

		// Token: 0x0400014E RID: 334
		private bool lastPartEndingBoundaryInserted;

		// Token: 0x0400014F RID: 335
		private List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

		// Token: 0x04000150 RID: 336
		private List<KeyValuePair<string, string>> extendedHeaders = new List<KeyValuePair<string, string>>();

		// Token: 0x04000151 RID: 337
		private MimeContentDescriptions contentDescriptions;
	}
}
