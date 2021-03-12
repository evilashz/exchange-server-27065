﻿using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000403 RID: 1027
	internal class FastIndexSmtpMessageContextBlob : SmtpMessageContextBlob
	{
		// Token: 0x06002F6C RID: 12140 RVA: 0x000BE07C File Offset: 0x000BC27C
		public static bool IsSupportedVersion(string ehloAdvertisement, out Version version)
		{
			version = null;
			if (ehloAdvertisement.Equals(FastIndexSmtpMessageContextBlob.VersionString, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			Match match = FastIndexSmtpMessageContextBlob.VersionRegex.Match(ehloAdvertisement);
			if (!match.Success)
			{
				return false;
			}
			version = new Version(int.Parse(match.Groups["Major"].Value), int.Parse(match.Groups["Minor"].Value), int.Parse(match.Groups["Build"].Value), int.Parse(match.Groups["Revision"].Value));
			return FastIndexSmtpMessageContextBlob.Version.Major == version.Major && FastIndexSmtpMessageContextBlob.Version.Minor == version.Minor;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000BE149 File Offset: 0x000BC349
		public FastIndexSmtpMessageContextBlob(bool acceptFromSmptIn, bool sendToSmtpOut, ProcessTransportRole role) : base(acceptFromSmptIn, sendToSmtpOut, role)
		{
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000BE154 File Offset: 0x000BC354
		public override string Name
		{
			get
			{
				return FastIndexSmtpMessageContextBlob.VersionString;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000BE15B File Offset: 0x000BC35B
		public override ByteQuantifiedSize MaxBlobSize
		{
			get
			{
				return Components.TransportAppConfig.MessageContextBlobConfiguration.FastIndexMaxBlobSize;
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000BE16C File Offset: 0x000BC36C
		public override bool IsAdvertised(IEhloOptions ehloOptions)
		{
			return ehloOptions.XFastIndex;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000BE174 File Offset: 0x000BC374
		public override void DeserializeBlob(Stream stream, ISmtpInSession smtpInSession, long blobSize)
		{
			ArgumentValidator.ThrowIfNull("stream", stream);
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			ArgumentValidator.ThrowIfNull("transportMailItem", smtpInSession.TransportMailItem);
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DeserializeFastIndexBlob);
			this.DeserializeBlobInternal(stream, smtpInSession.TransportMailItem, blobSize);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		public override void DeserializeBlob(Stream stream, SmtpInSessionState sessionState, long blobSize)
		{
			ArgumentValidator.ThrowIfNull("stream", stream);
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("transportMailItem", sessionState.TransportMailItem);
			this.DeserializeBlobInternal(stream, sessionState.TransportMailItem, blobSize);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000BE1F8 File Offset: 0x000BC3F8
		public override Stream SerializeBlob(SmtpOutSession smtpOutSession)
		{
			smtpOutSession.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SerializeFastIndexBlob);
			LazyBytes fastIndexBlob = smtpOutSession.RoutedMailItem.FastIndexBlob;
			int num = (fastIndexBlob.Value == null) ? 0 : fastIndexBlob.Value.Length;
			byte[] bytes = BitConverter.GetBytes(num);
			Stream stream = new MultiByteArrayMemoryStream();
			stream.Write(bytes, 0, bytes.Length);
			if (fastIndexBlob.Value != null && fastIndexBlob.Value.Length > 0)
			{
				stream.Write(fastIndexBlob.Value, 0, fastIndexBlob.Value.Length);
			}
			SystemProbeHelper.SmtpSendTracer.TracePass<int>(smtpOutSession.RoutedMailItem, 0L, "Sending fast Index. Size={0}", num);
			return stream;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000BE286 File Offset: 0x000BC486
		public override bool VerifyPermission(Permission permission)
		{
			return SmtpInSessionUtils.HasSMTPAcceptXMessageContextFastIndexPermission(permission);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000BE28E File Offset: 0x000BC48E
		public override bool HasDataToSend(SmtpOutSession smtpOutSession)
		{
			if (smtpOutSession.RoutedMailItem.FastIndexBlob.Value != null && smtpOutSession.RoutedMailItem.FastIndexBlob.Value.Length != 0)
			{
				return true;
			}
			smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "FastIndex blob is empty. Not sending the blob.");
			return false;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000BE2CC File Offset: 0x000BC4CC
		private void DeserializeBlobInternal(Stream stream, TransportMailItem transportMailItem, long blobSize)
		{
			byte[] intValueReadBuffer = new byte[4];
			int num = SmtpMessageContextBlob.ReadInt(stream, intValueReadBuffer);
			if ((long)num != blobSize - 4L)
			{
				throw new FormatException(string.Format("Unexpected blob size while processing FastIndex blob. Expected value is {0}. Actual value is {1}", blobSize - 4L, num));
			}
			transportMailItem.FastIndexBlob.Value = new byte[num];
			stream.Read(transportMailItem.FastIndexBlob.Value, 0, num);
			SystemProbeHelper.SmtpReceiveTracer.TracePass<int>(transportMailItem, 0L, "Receieved fast Index information. Size={0}", num);
		}

		// Token: 0x0400174F RID: 5967
		public static readonly Version Version = new Version(1, 1, 0, 0);

		// Token: 0x04001750 RID: 5968
		public static readonly string FastIndexBlobName = "FSTINDX";

		// Token: 0x04001751 RID: 5969
		private static readonly Regex VersionRegex = new Regex("^FSTINDX-(?<Major>\\d{1,7})\\.(?<Minor>\\d{1,7})\\.(?<Build>\\d{1,7})\\.(?<Revision>\\d{1,7})$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001752 RID: 5970
		public static readonly string VersionString = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
		{
			FastIndexSmtpMessageContextBlob.FastIndexBlobName,
			FastIndexSmtpMessageContextBlob.Version
		});

		// Token: 0x04001753 RID: 5971
		public static readonly string VersionStringWithSpace = string.Format(CultureInfo.InvariantCulture, " {0}", new object[]
		{
			FastIndexSmtpMessageContextBlob.VersionString
		});
	}
}
