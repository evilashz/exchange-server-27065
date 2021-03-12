using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Null
{
	// Token: 0x020000FF RID: 255
	internal class MessagingDatabase : IMessagingDatabase
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00026197 File Offset: 0x00024397
		public DataSource DataSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002619A File Offset: 0x0002439A
		public ServerInfoTable ServerInfoTable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000261A1 File Offset: 0x000243A1
		public QueueTable QueueTable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000261A8 File Offset: 0x000243A8
		public void SuspendDataCleanup()
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000261AA File Offset: 0x000243AA
		public void ResumeDataCleanup()
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000261AC File Offset: 0x000243AC
		public IMailRecipientStorage NewRecipientStorage(long messageId)
		{
			return new MessagingDatabase.MailRecipientStorage
			{
				MsgId = messageId
			};
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000261C7 File Offset: 0x000243C7
		public IMailItemStorage NewMailItemStorage(bool loadDefaults)
		{
			return new MessagingDatabase.MailItemStorage();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000261CE File Offset: 0x000243CE
		public IMailItemStorage LoadMailItemFromId(long msgId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000261D5 File Offset: 0x000243D5
		public IEnumerable<IMailRecipientStorage> LoadMailRecipientsFromMessageId(long messageId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000261DC File Offset: 0x000243DC
		public IMailRecipientStorage LoadMailRecipientFromId(long recipientId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000261E3 File Offset: 0x000243E3
		public Transaction BeginNewTransaction()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000261EA File Offset: 0x000243EA
		public void Attach(IMessagingDatabaseConfig config)
		{
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000261EC File Offset: 0x000243EC
		public void Detach()
		{
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000261EE File Offset: 0x000243EE
		public IReplayRequest NewReplayRequest(Guid correlationId, Destination destination, DateTime startTime, DateTime endTime, bool isTestRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000261F5 File Offset: 0x000243F5
		public IEnumerable<IReplayRequest> GetAllReplayRequests()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000261FC File Offset: 0x000243FC
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return new XElement("NullMessagingDatabase");
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002620D File Offset: 0x0002440D
		public virtual IEnumerable<MailItemAndRecipients> GetMessages(List<long> messageIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00026214 File Offset: 0x00024414
		public void BootLoadCompleted()
		{
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00026216 File Offset: 0x00024416
		public virtual MessagingDatabaseResultStatus ReadUnprocessedMessageIds(out Dictionary<byte, List<long>> unprocessedMessageIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002621D File Offset: 0x0002441D
		public void Start()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00026224 File Offset: 0x00024424
		public string CurrentState
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x02000100 RID: 256
		internal class MailItemStorage : IMailItemStorage
		{
			// Token: 0x06000A64 RID: 2660 RVA: 0x00026234 File Offset: 0x00024434
			public MailItemStorage()
			{
				this.IsNew = true;
				this.mimeCache = new MimeCache(this);
				this.XExch50Blob = new XExch50Blob();
				this.FastIndexBlob = new LazyBytes();
				this.ExtendedProperties = new ExtendedPropertyDictionary();
				this.TransportPropertiesHeader = new MultiValueHeader(this, "X-MS-Exchange-Organization-Transport-Properties");
				this.IsActive = true;
				this.DateReceived = DateTime.UtcNow;
				this.MimeNotSequential = false;
				this.FromAddress = string.Empty;
				this.MimeFrom = string.Empty;
				this.MimeSenderAddress = string.Empty;
				this.DsnFormat = DsnFormat.Default;
				this.HeloDomain = null;
				this.EnvId = string.Empty;
				this.Auth = string.Empty;
				this.BodyType = BodyType.Default;
				this.ReceiveConnectorName = string.Empty;
				this.Subject = string.Empty;
				this.InternetMessageId = string.Empty;
				this.ShadowServerDiscardId = string.Empty;
				this.Directionality = MailDirectionality.Undefined;
				this.ShadowMessageId = Guid.NewGuid();
				this.SourceIPAddress = IPAddress.None;
				this.PoisonCount = 0;
			}

			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00026341 File Offset: 0x00024541
			// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00026349 File Offset: 0x00024549
			public bool IsNew { get; private set; }

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00026352 File Offset: 0x00024552
			// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002635A File Offset: 0x0002455A
			public bool IsDeleted { get; private set; }

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00026363 File Offset: 0x00024563
			// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0002636B File Offset: 0x0002456B
			public bool IsActive { get; set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00026374 File Offset: 0x00024574
			// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0002637C File Offset: 0x0002457C
			public bool IsReadOnly { get; set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00026385 File Offset: 0x00024585
			// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0002638D File Offset: 0x0002458D
			public bool PendingDatabaseUpdates { get; private set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00026396 File Offset: 0x00024596
			// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0002639E File Offset: 0x0002459E
			public bool IsInAsyncCommit { get; private set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000263A7 File Offset: 0x000245A7
			// (set) Token: 0x06000A72 RID: 2674 RVA: 0x000263AF File Offset: 0x000245AF
			public long MsgId { get; private set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000A73 RID: 2675 RVA: 0x000263B8 File Offset: 0x000245B8
			// (set) Token: 0x06000A74 RID: 2676 RVA: 0x000263C0 File Offset: 0x000245C0
			public string FromAddress { get; set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000263C9 File Offset: 0x000245C9
			// (set) Token: 0x06000A76 RID: 2678 RVA: 0x000263D1 File Offset: 0x000245D1
			public string AttributedFromAddress { get; set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000263DA File Offset: 0x000245DA
			// (set) Token: 0x06000A78 RID: 2680 RVA: 0x000263E2 File Offset: 0x000245E2
			public DateTime DateReceived { get; set; }

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000A79 RID: 2681 RVA: 0x000263EB File Offset: 0x000245EB
			// (set) Token: 0x06000A7A RID: 2682 RVA: 0x000263F3 File Offset: 0x000245F3
			public TimeSpan ExtensionToExpiryDuration { get; set; }

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000263FC File Offset: 0x000245FC
			// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00026404 File Offset: 0x00024604
			public DsnFormat DsnFormat { get; set; }

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002640D File Offset: 0x0002460D
			// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00026415 File Offset: 0x00024615
			public bool IsDiscardPending { get; set; }

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002641E File Offset: 0x0002461E
			// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00026426 File Offset: 0x00024626
			public MailDirectionality Directionality { get; set; }

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002642F File Offset: 0x0002462F
			// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00026437 File Offset: 0x00024637
			public string HeloDomain { get; set; }

			// Token: 0x170002C1 RID: 705
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00026440 File Offset: 0x00024640
			// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00026448 File Offset: 0x00024648
			public string Auth { get; set; }

			// Token: 0x170002C2 RID: 706
			// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00026451 File Offset: 0x00024651
			// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00026459 File Offset: 0x00024659
			public string EnvId { get; set; }

			// Token: 0x170002C3 RID: 707
			// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00026462 File Offset: 0x00024662
			// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002646A File Offset: 0x0002466A
			public BodyType BodyType { get; set; }

			// Token: 0x170002C4 RID: 708
			// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00026473 File Offset: 0x00024673
			// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002647B File Offset: 0x0002467B
			public string Oorg { get; set; }

			// Token: 0x170002C5 RID: 709
			// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00026484 File Offset: 0x00024684
			// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002648C File Offset: 0x0002468C
			public string ReceiveConnectorName { get; set; }

			// Token: 0x170002C6 RID: 710
			// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00026495 File Offset: 0x00024695
			// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0002649D File Offset: 0x0002469D
			public int PoisonCount { get; set; }

			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000264A6 File Offset: 0x000246A6
			// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000264AE File Offset: 0x000246AE
			public IPAddress SourceIPAddress { get; set; }

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000A91 RID: 2705 RVA: 0x000264B7 File Offset: 0x000246B7
			// (set) Token: 0x06000A92 RID: 2706 RVA: 0x000264BF File Offset: 0x000246BF
			public string Subject { get; set; }

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000264C8 File Offset: 0x000246C8
			// (set) Token: 0x06000A94 RID: 2708 RVA: 0x000264D0 File Offset: 0x000246D0
			public string InternetMessageId { get; set; }

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000264D9 File Offset: 0x000246D9
			// (set) Token: 0x06000A96 RID: 2710 RVA: 0x000264E1 File Offset: 0x000246E1
			public Guid ShadowMessageId { get; set; }

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000264EA File Offset: 0x000246EA
			// (set) Token: 0x06000A98 RID: 2712 RVA: 0x000264F2 File Offset: 0x000246F2
			public string ShadowServerContext { get; set; }

			// Token: 0x170002CC RID: 716
			// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000264FB File Offset: 0x000246FB
			// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00026503 File Offset: 0x00024703
			public string ShadowServerDiscardId { get; set; }

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002650C File Offset: 0x0002470C
			// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00026514 File Offset: 0x00024714
			public IDataExternalComponent Recipients { get; set; }

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002651D File Offset: 0x0002471D
			// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00026525 File Offset: 0x00024725
			public int Scl { get; set; }

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002652E File Offset: 0x0002472E
			// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00026536 File Offset: 0x00024736
			public string PerfCounterAttribution { get; set; }

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002653F File Offset: 0x0002473F
			// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00026547 File Offset: 0x00024747
			public IExtendedPropertyCollection ExtendedProperties { get; private set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00026550 File Offset: 0x00024750
			// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00026558 File Offset: 0x00024758
			public XExch50Blob XExch50Blob { get; private set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00026561 File Offset: 0x00024761
			// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00026569 File Offset: 0x00024769
			public LazyBytes FastIndexBlob { get; private set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00026572 File Offset: 0x00024772
			// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0002657A File Offset: 0x0002477A
			public bool IsJournalReport { get; private set; }

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00026583 File Offset: 0x00024783
			// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0002658B File Offset: 0x0002478B
			public List<string> MoveToHosts { get; set; }

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00026594 File Offset: 0x00024794
			// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0002659C File Offset: 0x0002479C
			public bool RetryDeliveryIfRejected { get; set; }

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000265A5 File Offset: 0x000247A5
			// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000265AD File Offset: 0x000247AD
			public MultiValueHeader TransportPropertiesHeader { get; private set; }

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000265B6 File Offset: 0x000247B6
			// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000265BE File Offset: 0x000247BE
			public DeliveryPriority? Priority { get; set; }

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000265C7 File Offset: 0x000247C7
			// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000265CF File Offset: 0x000247CF
			public DeliveryPriority BootloadingPriority { get; set; }

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000265D8 File Offset: 0x000247D8
			// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x000265E0 File Offset: 0x000247E0
			public string PrioritizationReason { get; set; }

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000265E9 File Offset: 0x000247E9
			// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x000265F1 File Offset: 0x000247F1
			public Guid NetworkMessageId { get; set; }

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000265FA File Offset: 0x000247FA
			// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00026602 File Offset: 0x00024802
			public Guid SystemProbeId { get; set; }

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002660B File Offset: 0x0002480B
			// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00026613 File Offset: 0x00024813
			public RiskLevel RiskLevel { get; set; }

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002661C File Offset: 0x0002481C
			// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00026624 File Offset: 0x00024824
			public string ExoAccountForest { get; set; }

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002662D File Offset: 0x0002482D
			// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00026635 File Offset: 0x00024835
			public string ExoTenantContainer { get; set; }

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002663E File Offset: 0x0002483E
			// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00026646 File Offset: 0x00024846
			public Guid ExternalOrganizationId { get; set; }

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002664F File Offset: 0x0002484F
			// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0002665C File Offset: 0x0002485C
			public MimeDocument MimeDocument
			{
				get
				{
					return this.mimeCache.MimeDocument;
				}
				set
				{
					this.mimeCache.SetMimeDocument(value);
				}
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002666A File Offset: 0x0002486A
			public EmailMessage Message
			{
				get
				{
					return this.mimeCache.Message;
				}
			}

			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00026677 File Offset: 0x00024877
			// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002667F File Offset: 0x0002487F
			public string MimeFrom { get; set; }

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00026688 File Offset: 0x00024888
			// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00026690 File Offset: 0x00024890
			public string MimeSenderAddress { get; set; }

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00026699 File Offset: 0x00024899
			// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x000266A1 File Offset: 0x000248A1
			public bool MimeNotSequential { get; set; }

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000266AA File Offset: 0x000248AA
			// (set) Token: 0x06000ACB RID: 2763 RVA: 0x000266B7 File Offset: 0x000248B7
			public bool FallbackToRawOverride
			{
				get
				{
					return this.mimeCache.FallbackToRawOverride;
				}
				set
				{
					this.mimeCache.FallbackToRawOverride = value;
				}
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000266C5 File Offset: 0x000248C5
			// (set) Token: 0x06000ACD RID: 2765 RVA: 0x000266D2 File Offset: 0x000248D2
			public Encoding MimeDefaultEncoding
			{
				get
				{
					return this.mimeCache.DefaultEncoding;
				}
				set
				{
					this.mimeCache.DefaultEncoding = value;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000266E0 File Offset: 0x000248E0
			public bool MimeWriteStreamOpen
			{
				get
				{
					return this.mimeCache.MimeWriteStreamOpen;
				}
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000266ED File Offset: 0x000248ED
			// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x000266F5 File Offset: 0x000248F5
			public long MimeSize { get; set; }

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x000266FE File Offset: 0x000248FE
			public MimePart RootPart
			{
				get
				{
					return this.mimeCache.RootPart;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002670B File Offset: 0x0002490B
			// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00026713 File Offset: 0x00024913
			public string ProbeName { get; set; }

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002671C File Offset: 0x0002491C
			// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00026724 File Offset: 0x00024924
			public bool PersistProbeTrace { get; set; }

			// Token: 0x06000AD6 RID: 2774 RVA: 0x0002672D File Offset: 0x0002492D
			public Stream OpenMimeReadStream(bool downConvert)
			{
				return this.mimeCache.OpenMimeReadStream(downConvert);
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x0002673B File Offset: 0x0002493B
			public Stream OpenMimeWriteStream(MimeLimits mimeLimits, bool expectBinaryContent)
			{
				return this.mimeCache.OpenMimeWriteStream(mimeLimits, expectBinaryContent);
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0002674A File Offset: 0x0002494A
			public void OpenMimeDBWriter(DataTableCursor cursor, bool update, Func<bool> checkpointCallback, out Stream mimeMap, out CreateFixedStream mimeCreateFixedStream)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00026751 File Offset: 0x00024951
			public Stream OpenMimeDBReader()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x00026758 File Offset: 0x00024958
			public MimeDocument LoadMimeFromDB(DecodingOptions decodingOptions)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x0002675F File Offset: 0x0002495F
			public long GetCurrrentMimeSize()
			{
				return this.mimeCache.MimeStreamSize;
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0002676C File Offset: 0x0002496C
			public long RefreshMimeSize()
			{
				return this.mimeCache.MimeStreamSize;
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x00026779 File Offset: 0x00024979
			public void UpdateCachedHeaders()
			{
				this.mimeCache.PromoteHeaders();
			}

			// Token: 0x06000ADE RID: 2782 RVA: 0x00026786 File Offset: 0x00024986
			public void RestoreLastSavedMime()
			{
				this.mimeCache.RestoreLastSavedMime();
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00026793 File Offset: 0x00024993
			public void ResetMimeParserEohCallback()
			{
				this.mimeCache.ResetMimeParserEohCallback();
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x000267A0 File Offset: 0x000249A0
			public void MinimizeMemory()
			{
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x000267A2 File Offset: 0x000249A2
			public void Commit(TransactionCommitMode commitMode)
			{
				this.Materialize(null);
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x000267AB File Offset: 0x000249AB
			public void Materialize(Transaction transaction)
			{
				if (this.MsgId == 0L)
				{
					this.MsgId = Interlocked.Increment(ref MessagingDatabase.MailItemStorage.lastId);
				}
				this.UpdateCachedHeaders();
				this.IsNew = false;
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x000267D4 File Offset: 0x000249D4
			public IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState)
			{
				this.Commit(TransactionCommitMode.Lazy);
				return new AsyncResult(asyncCallback, asyncState, true);
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x000267E5 File Offset: 0x000249E5
			public bool EndCommit(IAsyncResult ar, out Exception exception)
			{
				exception = null;
				return true;
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x000267EB File Offset: 0x000249EB
			public void MarkToDelete()
			{
				this.IsDeleted = true;
				this.IsNew = false;
			}

			// Token: 0x06000AE6 RID: 2790 RVA: 0x000267FB File Offset: 0x000249FB
			public IMailItemStorage Clone()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000AE7 RID: 2791 RVA: 0x00026802 File Offset: 0x00024A02
			public void ReleaseFromActive()
			{
				this.IsActive = false;
				this.mimeCache.CleanupEmailMessage();
				this.mimeCache.CleanupMimeDocument();
			}

			// Token: 0x06000AE8 RID: 2792 RVA: 0x00026821 File Offset: 0x00024A21
			public IMailItemStorage CloneCommitted(Action<IMailItemStorage> cloneVisitor)
			{
				return this.Clone();
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x00026829 File Offset: 0x00024A29
			public void AtomicChange(Action<IMailItemStorage> changeAction)
			{
				changeAction(this);
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x00026832 File Offset: 0x00024A32
			public IMailItemStorage CopyCommitted(Action<IMailItemStorage> copyVisitor)
			{
				return this.Clone();
			}

			// Token: 0x04000480 RID: 1152
			private static long lastId;

			// Token: 0x04000481 RID: 1153
			private readonly MimeCache mimeCache;
		}

		// Token: 0x02000101 RID: 257
		internal class MailRecipientStorage : IMailRecipientStorage
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x0002683C File Offset: 0x00024A3C
			public MailRecipientStorage()
			{
				this.ExtendedProperties = new ExtendedPropertyDictionary();
				this.Email = string.Empty;
				this.DsnRequested = DsnRequestedFlags.Default;
				this.DsnNeeded = DsnFlags.None;
				this.DsnCompleted = DsnFlags.None;
				this.Status = Status.Ready;
				this.RetryCount = 0;
				this.AdminActionStatus = AdminActionStatus.None;
				this.IsActive = true;
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00026896 File Offset: 0x00024A96
			// (set) Token: 0x06000AED RID: 2797 RVA: 0x0002689E File Offset: 0x00024A9E
			public long RecipId { get; private set; }

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000268A7 File Offset: 0x00024AA7
			// (set) Token: 0x06000AEF RID: 2799 RVA: 0x000268AF File Offset: 0x00024AAF
			public long MsgId { get; set; }

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000268B8 File Offset: 0x00024AB8
			// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x000268C0 File Offset: 0x00024AC0
			public AdminActionStatus AdminActionStatus { get; set; }

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000268C9 File Offset: 0x00024AC9
			// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x000268D1 File Offset: 0x00024AD1
			public DateTime? DeliveryTime { get; set; }

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000268DA File Offset: 0x00024ADA
			// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x000268E2 File Offset: 0x00024AE2
			public DsnFlags DsnCompleted { get; set; }

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000268EB File Offset: 0x00024AEB
			// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x000268F3 File Offset: 0x00024AF3
			public DsnFlags DsnNeeded { get; set; }

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000268FC File Offset: 0x00024AFC
			// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00026904 File Offset: 0x00024B04
			public DsnRequestedFlags DsnRequested { get; set; }

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002690D File Offset: 0x00024B0D
			// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00026915 File Offset: 0x00024B15
			public Destination DeliveredDestination { get; set; }

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0002691E File Offset: 0x00024B1E
			// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00026926 File Offset: 0x00024B26
			public string Email { get; set; }

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002692F File Offset: 0x00024B2F
			// (set) Token: 0x06000AFF RID: 2815 RVA: 0x00026937 File Offset: 0x00024B37
			public string ORcpt { get; set; }

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00026940 File Offset: 0x00024B40
			// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00026948 File Offset: 0x00024B48
			public string PrimaryServerFqdnGuid { get; set; }

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00026951 File Offset: 0x00024B51
			// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00026959 File Offset: 0x00024B59
			public int RetryCount { get; set; }

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00026962 File Offset: 0x00024B62
			// (set) Token: 0x06000B05 RID: 2821 RVA: 0x0002696A File Offset: 0x00024B6A
			public Status Status { get; set; }

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00026973 File Offset: 0x00024B73
			// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0002697B File Offset: 0x00024B7B
			public RequiredTlsAuthLevel? TlsAuthLevel { get; set; }

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00026984 File Offset: 0x00024B84
			// (set) Token: 0x06000B09 RID: 2825 RVA: 0x0002698C File Offset: 0x00024B8C
			public int OutboundIPPool { get; set; }

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00026995 File Offset: 0x00024B95
			// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0002699D File Offset: 0x00024B9D
			public IExtendedPropertyCollection ExtendedProperties { get; private set; }

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000269A6 File Offset: 0x00024BA6
			// (set) Token: 0x06000B0D RID: 2829 RVA: 0x000269AE File Offset: 0x00024BAE
			public bool IsDeleted { get; private set; }

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000269B7 File Offset: 0x00024BB7
			// (set) Token: 0x06000B0F RID: 2831 RVA: 0x000269BF File Offset: 0x00024BBF
			public bool IsInSafetyNet { get; private set; }

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000269C8 File Offset: 0x00024BC8
			// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000269D0 File Offset: 0x00024BD0
			public bool IsActive { get; private set; }

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000269D9 File Offset: 0x00024BD9
			public bool PendingDatabaseUpdates
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x000269DC File Offset: 0x00024BDC
			public void MarkToDelete()
			{
				this.IsDeleted = true;
				this.IsActive = false;
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x000269EC File Offset: 0x00024BEC
			public void Materialize(Transaction transaction)
			{
				if (this.RecipId == 0L)
				{
					this.RecipId = Interlocked.Increment(ref MessagingDatabase.MailRecipientStorage.lastRecipId);
				}
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x00026A08 File Offset: 0x00024C08
			public void Commit(TransactionCommitMode commitMode)
			{
				this.Materialize(null);
			}

			// Token: 0x06000B16 RID: 2838 RVA: 0x00026A11 File Offset: 0x00024C11
			public IMailRecipientStorage MoveTo(long targetMsgId)
			{
				this.MsgId = targetMsgId;
				return this;
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x00026A1B File Offset: 0x00024C1B
			public IMailRecipientStorage CopyTo(long target)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x00026A22 File Offset: 0x00024C22
			public void MinimizeMemory()
			{
			}

			// Token: 0x06000B19 RID: 2841 RVA: 0x00026A24 File Offset: 0x00024C24
			public void ReleaseFromActive()
			{
				this.IsActive = false;
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x00026A2D File Offset: 0x00024C2D
			public void AddToSafetyNet()
			{
				this.IsInSafetyNet = true;
			}

			// Token: 0x040004B6 RID: 1206
			private static long lastRecipId;
		}
	}
}
