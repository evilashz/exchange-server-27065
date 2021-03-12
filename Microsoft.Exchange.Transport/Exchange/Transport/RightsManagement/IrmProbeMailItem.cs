using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003F0 RID: 1008
	internal sealed class IrmProbeMailItem : IReadOnlyMailItem, ISystemProbeTraceable
	{
		// Token: 0x06002DCE RID: 11726 RVA: 0x000B874A File Offset: 0x000B694A
		public static IrmProbeMailItem CreateFromStream(Stream stream)
		{
			return new IrmProbeMailItem(stream);
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000B8752 File Offset: 0x000B6952
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B8755 File Offset: 0x000B6955
		public string Auth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000B875C File Offset: 0x000B695C
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000B8763 File Offset: 0x000B6963
		public BodyType BodyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000B876A File Offset: 0x000B696A
		public DateTime DateReceived
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000B8771 File Offset: 0x000B6971
		public DeferReason DeferReason
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x000B8778 File Offset: 0x000B6978
		public MailDirectionality Directionality
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x000B877F File Offset: 0x000B697F
		public DsnFormat DsnFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x000B8786 File Offset: 0x000B6986
		public DsnParameters DsnParameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000B878D File Offset: 0x000B698D
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x000B8794 File Offset: 0x000B6994
		public bool SuppressBodyInDsn
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000B879B File Offset: 0x000B699B
		public string EnvId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x000B87A2 File Offset: 0x000B69A2
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.extendedPropertyDictionary;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x000B87AA File Offset: 0x000B69AA
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000B87B1 File Offset: 0x000B69B1
		public RoutingAddress From
		{
			get
			{
				return RoutingAddress.Empty;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000B87B8 File Offset: 0x000B69B8
		public string HeloDomain
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000B87BF File Offset: 0x000B69BF
		public string InternetMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000B87C6 File Offset: 0x000B69C6
		public Guid NetworkMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000B87CD File Offset: 0x000B69CD
		public bool IsActive
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000B87D4 File Offset: 0x000B69D4
		public bool IsHeartbeat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000B87DB File Offset: 0x000B69DB
		public LatencyTracker LatencyTracker
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000B87DE File Offset: 0x000B69DE
		public byte[] LegacyXexch50Blob
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000B87E5 File Offset: 0x000B69E5
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000B87EC File Offset: 0x000B69EC
		public EmailMessage Message
		{
			get
			{
				return this.emailMessage;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000B87F4 File Offset: 0x000B69F4
		public string Oorg
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000B87FB File Offset: 0x000B69FB
		public string ExoAccountForest
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000B8802 File Offset: 0x000B6A02
		public string ExoTenantContainer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000B8809 File Offset: 0x000B6A09
		public Guid ExternalOrganizationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000B8810 File Offset: 0x000B6A10
		public RiskLevel RiskLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000B8817 File Offset: 0x000B6A17
		public DeliveryPriority Priority
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000B881E File Offset: 0x000B6A1E
		public string PrioritizationReason
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000B8825 File Offset: 0x000B6A25
		public MimeDocument MimeDocument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000B8828 File Offset: 0x000B6A28
		public string MimeFrom
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000B882F File Offset: 0x000B6A2F
		public RoutingAddress MimeSender
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000B8836 File Offset: 0x000B6A36
		public long MimeSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000B883D File Offset: 0x000B6A3D
		public OrganizationId OrganizationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000B8844 File Offset: 0x000B6A44
		public RoutingAddress OriginalFrom
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000B884B File Offset: 0x000B6A4B
		public int PoisonCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000B8852 File Offset: 0x000B6A52
		public int PoisonForRemoteCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000B8859 File Offset: 0x000B6A59
		public bool IsPoison
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000B8860 File Offset: 0x000B6A60
		public string ReceiveConnectorName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000B8867 File Offset: 0x000B6A67
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000B886A File Offset: 0x000B6A6A
		public long RecordId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000B8871 File Offset: 0x000B6A71
		public bool RetryDeliveryIfRejected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x000B8878 File Offset: 0x000B6A78
		public MimePart RootPart
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000B887F File Offset: 0x000B6A7F
		public int Scl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000B8886 File Offset: 0x000B6A86
		public Guid ShadowMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000B888D File Offset: 0x000B6A8D
		public string ShadowServerContext
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000B8894 File Offset: 0x000B6A94
		public string ShadowServerDiscardId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000B889B File Offset: 0x000B6A9B
		public IPAddress SourceIPAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000B88A2 File Offset: 0x000B6AA2
		public string Subject
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000B88A9 File Offset: 0x000B6AA9
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000B88B0 File Offset: 0x000B6AB0
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x000B88B7 File Offset: 0x000B6AB7
		public LazyBytes FastIndexBlob
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000B88BE File Offset: 0x000B6ABE
		public void CacheTransportSettings()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B88C5 File Offset: 0x000B6AC5
		public bool IsJournalReport()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B88CC File Offset: 0x000B6ACC
		public bool IsPfReplica()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B88D3 File Offset: 0x000B6AD3
		public bool IsShadowed()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B88DA File Offset: 0x000B6ADA
		public bool IsDelayedAck()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000B88E1 File Offset: 0x000B6AE1
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000B88E8 File Offset: 0x000B6AE8
		public Stream OpenMimeReadStream()
		{
			return this.emailMessage.MimeDocument.RootPart.GetContentReadStream();
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000B88FF File Offset: 0x000B6AFF
		public Stream OpenMimeReadStream(bool downConvert)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000B8906 File Offset: 0x000B6B06
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000B890D File Offset: 0x000B6B0D
		public void AddDsnParameters(string key, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000B8914 File Offset: 0x000B6B14
		public void IncrementPoisonForRemoteCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000B891B File Offset: 0x000B6B1B
		public Guid SystemProbeId
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000B8922 File Offset: 0x000B6B22
		public bool IsProbe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000B8925 File Offset: 0x000B6B25
		public string ProbeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000B892C File Offset: 0x000B6B2C
		public bool PersistProbeTrace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000B892F File Offset: 0x000B6B2F
		private IrmProbeMailItem(Stream stream)
		{
			this.emailMessage = EmailMessage.Create(stream);
		}

		// Token: 0x040016D0 RID: 5840
		private readonly EmailMessage emailMessage;

		// Token: 0x040016D1 RID: 5841
		private readonly ExtendedPropertyDictionary extendedPropertyDictionary = new ExtendedPropertyDictionary();
	}
}
