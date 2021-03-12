using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000401 RID: 1025
	internal static class EnumConverter
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x000BD554 File Offset: 0x000BB754
		internal static DsnFormat PublicToInternal(DsnFormatRequested dsnFormatRequested)
		{
			DsnFormat result = DsnFormat.Default;
			switch (dsnFormatRequested)
			{
			case DsnFormatRequested.NotSpecified:
				result = DsnFormat.Default;
				break;
			case DsnFormatRequested.Full:
				result = DsnFormat.Full;
				break;
			case DsnFormatRequested.Headers:
				result = DsnFormat.Headers;
				break;
			}
			return result;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000BD584 File Offset: 0x000BB784
		internal static DsnFormatRequested InternalToPublic(DsnFormat dsnFormat)
		{
			DsnFormatRequested result = DsnFormatRequested.NotSpecified;
			switch (dsnFormat)
			{
			case DsnFormat.Default:
				result = DsnFormatRequested.NotSpecified;
				break;
			case DsnFormat.Full:
				result = DsnFormatRequested.Full;
				break;
			case DsnFormat.Headers:
				result = DsnFormatRequested.Headers;
				break;
			}
			return result;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000BD5B4 File Offset: 0x000BB7B4
		internal static Microsoft.Exchange.Transport.BodyType PublicToInternal(Microsoft.Exchange.Data.Transport.Smtp.BodyType bodyType)
		{
			Microsoft.Exchange.Transport.BodyType result = Microsoft.Exchange.Transport.BodyType.Default;
			switch (bodyType)
			{
			case Microsoft.Exchange.Data.Transport.Smtp.BodyType.NotSpecified:
				result = Microsoft.Exchange.Transport.BodyType.Default;
				break;
			case Microsoft.Exchange.Data.Transport.Smtp.BodyType.SevenBit:
				result = Microsoft.Exchange.Transport.BodyType.SevenBit;
				break;
			case Microsoft.Exchange.Data.Transport.Smtp.BodyType.EightBitMime:
				result = Microsoft.Exchange.Transport.BodyType.EightBitMIME;
				break;
			case Microsoft.Exchange.Data.Transport.Smtp.BodyType.BinaryMime:
				result = Microsoft.Exchange.Transport.BodyType.BinaryMIME;
				break;
			}
			return result;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000BD5EC File Offset: 0x000BB7EC
		internal static Microsoft.Exchange.Data.Transport.Smtp.BodyType InternalToPublic(Microsoft.Exchange.Transport.BodyType bodyType)
		{
			Microsoft.Exchange.Data.Transport.Smtp.BodyType result = Microsoft.Exchange.Data.Transport.Smtp.BodyType.NotSpecified;
			switch (bodyType)
			{
			case Microsoft.Exchange.Transport.BodyType.Default:
				result = Microsoft.Exchange.Data.Transport.Smtp.BodyType.NotSpecified;
				break;
			case Microsoft.Exchange.Transport.BodyType.SevenBit:
				result = Microsoft.Exchange.Data.Transport.Smtp.BodyType.SevenBit;
				break;
			case Microsoft.Exchange.Transport.BodyType.EightBitMIME:
				result = Microsoft.Exchange.Data.Transport.Smtp.BodyType.EightBitMime;
				break;
			case Microsoft.Exchange.Transport.BodyType.BinaryMIME:
				result = Microsoft.Exchange.Data.Transport.Smtp.BodyType.BinaryMime;
				break;
			}
			return result;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000BD624 File Offset: 0x000BB824
		internal static DsnRequestedFlags PublicToInternal(DsnTypeRequested dsnRequestedFlags)
		{
			DsnRequestedFlags result = DsnRequestedFlags.Default;
			switch (dsnRequestedFlags)
			{
			case DsnTypeRequested.NotSpecified:
				result = DsnRequestedFlags.Default;
				break;
			case DsnTypeRequested.Success:
				result = DsnRequestedFlags.Success;
				break;
			case DsnTypeRequested.Failure:
				result = DsnRequestedFlags.Failure;
				break;
			case (DsnTypeRequested)3:
				result = (DsnRequestedFlags.Success | DsnRequestedFlags.Failure);
				break;
			case DsnTypeRequested.Delay:
				result = DsnRequestedFlags.Delay;
				break;
			case (DsnTypeRequested)5:
				result = (DsnRequestedFlags.Success | DsnRequestedFlags.Delay);
				break;
			case (DsnTypeRequested)6:
				result = (DsnRequestedFlags.Failure | DsnRequestedFlags.Delay);
				break;
			case (DsnTypeRequested)7:
				result = (DsnRequestedFlags.Success | DsnRequestedFlags.Failure | DsnRequestedFlags.Delay);
				break;
			case DsnTypeRequested.Never:
				result = DsnRequestedFlags.Never;
				break;
			}
			return result;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000BD684 File Offset: 0x000BB884
		internal static DsnTypeRequested InternalToPublic(DsnRequestedFlags dsnRequestedFlags)
		{
			DsnTypeRequested result = DsnTypeRequested.NotSpecified;
			switch (dsnRequestedFlags)
			{
			case DsnRequestedFlags.Default:
				result = DsnTypeRequested.NotSpecified;
				break;
			case DsnRequestedFlags.Success:
				result = DsnTypeRequested.Success;
				break;
			case DsnRequestedFlags.Failure:
				result = DsnTypeRequested.Failure;
				break;
			case DsnRequestedFlags.Success | DsnRequestedFlags.Failure:
				result = (DsnTypeRequested)3;
				break;
			case DsnRequestedFlags.Delay:
				result = DsnTypeRequested.Delay;
				break;
			case DsnRequestedFlags.Success | DsnRequestedFlags.Delay:
				result = (DsnTypeRequested)5;
				break;
			case DsnRequestedFlags.Failure | DsnRequestedFlags.Delay:
				result = (DsnTypeRequested)6;
				break;
			case DsnRequestedFlags.Success | DsnRequestedFlags.Failure | DsnRequestedFlags.Delay:
				result = (DsnTypeRequested)7;
				break;
			case DsnRequestedFlags.Never:
				result = DsnTypeRequested.Never;
				break;
			}
			return result;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000BD6E4 File Offset: 0x000BB8E4
		internal static ParsingStatus InternalToPublic(ParsingStatus parsingStatus)
		{
			ParsingStatus result = ParsingStatus.Error;
			switch (parsingStatus)
			{
			case ParsingStatus.ProtocolError:
				result = ParsingStatus.ProtocolError;
				break;
			case ParsingStatus.Error:
				result = ParsingStatus.Error;
				break;
			case ParsingStatus.MoreDataRequired:
				result = ParsingStatus.MoreDataRequired;
				break;
			case ParsingStatus.Complete:
				result = ParsingStatus.Complete;
				break;
			}
			return result;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000BD71C File Offset: 0x000BB91C
		internal static TlsAuthLevel? PublicToInternal(RequiredTlsAuthLevel? requiredTlsAuthLevel)
		{
			TlsAuthLevel? result = null;
			if (requiredTlsAuthLevel != null)
			{
				switch (requiredTlsAuthLevel.Value)
				{
				case RequiredTlsAuthLevel.EncryptionOnly:
					result = new TlsAuthLevel?(TlsAuthLevel.EncryptionOnly);
					break;
				case RequiredTlsAuthLevel.CertificateValidation:
					result = new TlsAuthLevel?(TlsAuthLevel.CertificateValidation);
					break;
				case RequiredTlsAuthLevel.DomainValidation:
					result = new TlsAuthLevel?(TlsAuthLevel.DomainValidation);
					break;
				}
			}
			return result;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000BD778 File Offset: 0x000BB978
		internal static RequiredTlsAuthLevel? InternalToPublic(TlsAuthLevel? tlsAuthLevel)
		{
			RequiredTlsAuthLevel? result = null;
			if (tlsAuthLevel != null)
			{
				switch (tlsAuthLevel.Value)
				{
				case TlsAuthLevel.EncryptionOnly:
					result = new RequiredTlsAuthLevel?(RequiredTlsAuthLevel.EncryptionOnly);
					break;
				case TlsAuthLevel.CertificateValidation:
					result = new RequiredTlsAuthLevel?(RequiredTlsAuthLevel.CertificateValidation);
					break;
				case TlsAuthLevel.DomainValidation:
					result = new RequiredTlsAuthLevel?(RequiredTlsAuthLevel.DomainValidation);
					break;
				}
			}
			return result;
		}
	}
}
