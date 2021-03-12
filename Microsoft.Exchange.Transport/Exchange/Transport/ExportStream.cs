using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200015F RID: 351
	internal sealed class ExportStream : Stream
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0003EAEF File Offset: 0x0003CCEF
		private ExportStream(Stream headerStream, Stream contentStream)
		{
			this.contentStream = contentStream;
			this.headerStream = headerStream;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003EB05 File Offset: 0x0003CD05
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003EB08 File Offset: 0x0003CD08
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003EB0B File Offset: 0x0003CD0B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003EB0E File Offset: 0x0003CD0E
		public override long Length
		{
			get
			{
				return this.headerStream.Length + this.contentStream.Length;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003EB27 File Offset: 0x0003CD27
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0003EB2F File Offset: 0x0003CD2F
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003EB38 File Offset: 0x0003CD38
		public static bool TryCreate(IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, bool foreignConnectorExport, out Stream exportedMessage)
		{
			exportedMessage = null;
			Stream stream = Streams.CreateTemporaryStorageStream();
			if (!ExportStream.TryWriteReplayXHeaders(stream, mailItem, recipients, foreignConnectorExport))
			{
				return false;
			}
			Stream stream2;
			if (foreignConnectorExport && HeaderFirewall.ContainsBlockedHeaders(mailItem.RootPart.Headers, ~RestrictedHeaderSet.MTA))
			{
				stream2 = Streams.CreateTemporaryStorageStream();
				mailItem.RootPart.WriteTo(stream2, null, new HeaderFirewall.OutputFilter(~RestrictedHeaderSet.MTA));
				stream2.Seek(0L, SeekOrigin.Begin);
			}
			else
			{
				stream2 = mailItem.OpenMimeReadStream();
			}
			ExportStream.WriteAsciiHeader(stream, ExportStream.XxheadersEndAsciiHeaderName, stream2.Length.ToString());
			exportedMessage = new ExportStream(stream, stream2);
			return true;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003EBC4 File Offset: 0x0003CDC4
		public static bool TryWriteReplayXHeaders(Stream stream, IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipients, bool foreignConnectorExport)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MailSmtpCommand.FormatCommand(stringBuilder, mailItem.From, new EhloOptions
			{
				XAttr = true
			}, Components.Configuration.AppConfig.SmtpMailCommandConfiguration, false, 0L, mailItem.Auth, mailItem.EnvId, mailItem.DsnFormat, mailItem.BodyType, mailItem.Directionality, mailItem.ExternalOrganizationId, mailItem.OrganizationId, mailItem.ExoAccountForest, mailItem.ExoTenantContainer);
			ExportStream.WriteAsciiHeader(stream, ExportStream.XsenderAsciiHeaderName, stringBuilder.ToString());
			bool flag = false;
			foreach (MailRecipient mailRecipient in recipients)
			{
				if (mailRecipient.Status != Status.Handled && mailRecipient.Status != Status.Complete)
				{
					ExportStream.WriteRecipient(stream, mailRecipient, !foreignConnectorExport);
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
			if (!foreignConnectorExport)
			{
				ExportStream.WriteAsciiHeader(stream, ExportStream.XcreatedByAsciiHeaderName, ExportStream.CreatedByString);
				if (!string.IsNullOrEmpty(mailItem.HeloDomain))
				{
					ExportStream.WriteAsciiHeader(stream, ExportStream.XheloDomainAsciiHeaderName, mailItem.HeloDomain);
				}
				if (mailItem.ExtendedProperties.Count > 0)
				{
					ExportStream.WriteExtendedProps(stream, ExportStream.XextendedPropsAsciiHeaderName, mailItem.ExtendedProperties);
				}
				if (mailItem.LegacyXexch50Blob != null && mailItem.LegacyXexch50Blob.Length > 0)
				{
					ExportStream.WriteBinary(stream, ExportStream.XlegacyExch50AsciiHeaderName, mailItem.LegacyXexch50Blob);
				}
				if (!string.IsNullOrEmpty(mailItem.ReceiveConnectorName))
				{
					ExportStream.WriteRfc2047Header(stream, "X-Source", mailItem.ReceiveConnectorName);
				}
				ExportStream.WriteAsciiHeader(stream, ExportStream.XsourceIPAddressAsciiHeaderName, mailItem.SourceIPAddress.ToString());
			}
			return true;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003ED58 File Offset: 0x0003CF58
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.position >= this.headerStream.Length)
			{
				this.headerStream.Position = this.headerStream.Length;
				this.contentStream.Position = this.position - this.headerStream.Length;
			}
			else
			{
				this.headerStream.Position = this.position;
				this.contentStream.Position = 0L;
			}
			int num = this.headerStream.Read(buffer, offset, count);
			if (num < count)
			{
				num += this.contentStream.Read(buffer, offset + num, count - num);
			}
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003EE05 File Offset: 0x0003D005
		public override void Write(byte[] array, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003EE0C File Offset: 0x0003D00C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003EE13 File Offset: 0x0003D013
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003EE1A File Offset: 0x0003D01A
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003EE24 File Offset: 0x0003D024
		public override void Close()
		{
			try
			{
				if (this.headerStream != null)
				{
					this.headerStream.Close();
					this.headerStream = null;
				}
				if (this.contentStream != null)
				{
					this.contentStream.Close();
					this.contentStream = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003EE80 File Offset: 0x0003D080
		private static void WriteRecipient(Stream headerStream, MailRecipient recipient, bool writeExtendedProps)
		{
			RoutingAddress routingAddress;
			bool flag = OrarGenerator.TryGetOrarAddress(recipient, out routingAddress);
			StringBuilder stringBuilder = new StringBuilder();
			RcptSmtpCommand.FormatCommand(stringBuilder, null, recipient.Email, null, recipient.ORcpt, recipient.DsnRequested, flag ? routingAddress : RoutingAddress.Empty, null, false);
			writeExtendedProps = (writeExtendedProps && recipient.ExtendedProperties.Count > 0);
			ExportStream.WriteAsciiHeader(headerStream, ExportStream.XreceiverAsciiHeaderName, stringBuilder.ToString(), !writeExtendedProps);
			if (writeExtendedProps)
			{
				ExportStream.WriteExtendedProps(headerStream, ExportStream.XextendedPropsAsciiParameter, recipient.ExtendedProperties);
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003EF04 File Offset: 0x0003D104
		private static void WriteRfc2047Header(Stream headerStream, string headerName, string headerValue)
		{
			TextHeader textHeader = new TextHeader(headerName, headerValue);
			textHeader.WriteTo(headerStream);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003EF21 File Offset: 0x0003D121
		private static void WriteAsciiHeader(Stream headerStream, byte[] headerName, string headerValue)
		{
			ExportStream.WriteAsciiHeader(headerStream, headerName, headerValue, true);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003EF2C File Offset: 0x0003D12C
		private static void WriteAsciiHeader(Stream headerStream, byte[] headerName, string headerValue, bool appendCRLF)
		{
			headerStream.Write(headerName, 0, headerName.Length);
			byte[] array;
			if (appendCRLF)
			{
				array = Util.AsciiStringToBytesAndAppendCRLF(headerValue);
			}
			else
			{
				array = Util.AsciiStringToBytes(headerValue);
			}
			headerStream.Write(array, 0, array.Length);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003EF64 File Offset: 0x0003D164
		private static void WriteExtendedProps(Stream headerStream, byte[] header, IReadOnlyExtendedPropertyCollection extendedProps)
		{
			headerStream.Write(header, 0, header.Length);
			using (EncoderStream encoderStream = new EncoderStream(Streams.CreateSuppressCloseWrapper(headerStream), new Base64Encoder(0), EncoderStreamAccess.Write))
			{
				extendedProps.Serialize(encoderStream);
			}
			headerStream.Write(Util.AsciiCRLF, 0, Util.AsciiCRLF.Length);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003EFC8 File Offset: 0x0003D1C8
		private static void WriteBinary(Stream headerStream, byte[] header, byte[] data)
		{
			headerStream.Write(header, 0, header.Length);
			using (EncoderStream encoderStream = new EncoderStream(Streams.CreateSuppressCloseWrapper(headerStream), new Base64Encoder(0), EncoderStreamAccess.Write))
			{
				encoderStream.Write(data, 0, data.Length);
			}
			headerStream.Write(Util.AsciiCRLF, 0, Util.AsciiCRLF.Length);
		}

		// Token: 0x040007AD RID: 1965
		public const string ProductName = "MSExchange";

		// Token: 0x040007AE RID: 1966
		public const string Xsender = "X-sender";

		// Token: 0x040007AF RID: 1967
		public const string Xreceiver = "X-Receiver";

		// Token: 0x040007B0 RID: 1968
		public const string XcreatedBy = "X-CreatedBy";

		// Token: 0x040007B1 RID: 1969
		public const string XheloDomain = "X-HeloDomain";

		// Token: 0x040007B2 RID: 1970
		public const string XextendedProps = "X-ExtendedProps";

		// Token: 0x040007B3 RID: 1971
		public const string XextendedPropsParameter = "X-ExtendedProps=";

		// Token: 0x040007B4 RID: 1972
		public const string XlegacyExch50 = "X-LegacyExch50";

		// Token: 0x040007B5 RID: 1973
		public const string XxheadersEnd = "X-EndOfInjectedXHeaders";

		// Token: 0x040007B6 RID: 1974
		public const string Xsource = "X-Source";

		// Token: 0x040007B7 RID: 1975
		public const string XsourceIPAddress = "X-SourceIPAddress";

		// Token: 0x040007B8 RID: 1976
		public static readonly int ProductVersion = Assembly.GetExecutingAssembly().GetName().Version.Major;

		// Token: 0x040007B9 RID: 1977
		public static readonly string CreatedByString = "MSExchange" + ExportStream.ProductVersion.ToString(CultureInfo.InvariantCulture);

		// Token: 0x040007BA RID: 1978
		private static readonly byte[] XsenderAsciiHeaderName = Util.AsciiStringToBytes("X-sender: ");

		// Token: 0x040007BB RID: 1979
		private static readonly byte[] XreceiverAsciiHeaderName = Util.AsciiStringToBytes("X-Receiver: ");

		// Token: 0x040007BC RID: 1980
		private static readonly byte[] XcreatedByAsciiHeaderName = Util.AsciiStringToBytes("X-CreatedBy: ");

		// Token: 0x040007BD RID: 1981
		private static readonly byte[] XheloDomainAsciiHeaderName = Util.AsciiStringToBytes("X-HeloDomain: ");

		// Token: 0x040007BE RID: 1982
		private static readonly byte[] XextendedPropsAsciiHeaderName = Util.AsciiStringToBytes("X-ExtendedProps: ");

		// Token: 0x040007BF RID: 1983
		private static readonly byte[] XextendedPropsAsciiParameter = Util.AsciiStringToBytes("; X-ExtendedProps=");

		// Token: 0x040007C0 RID: 1984
		private static readonly byte[] XlegacyExch50AsciiHeaderName = Util.AsciiStringToBytes("X-LegacyExch50: ");

		// Token: 0x040007C1 RID: 1985
		private static readonly byte[] XxheadersEndAsciiHeaderName = Util.AsciiStringToBytes("X-EndOfInjectedXHeaders: ");

		// Token: 0x040007C2 RID: 1986
		private static readonly byte[] XsourceAsciiHeaderName = Util.AsciiStringToBytes("X-Source: ");

		// Token: 0x040007C3 RID: 1987
		private static readonly byte[] XsourceIPAddressAsciiHeaderName = Util.AsciiStringToBytes("X-SourceIPAddress: ");

		// Token: 0x040007C4 RID: 1988
		private Stream headerStream;

		// Token: 0x040007C5 RID: 1989
		private Stream contentStream;

		// Token: 0x040007C6 RID: 1990
		private long position;
	}
}
