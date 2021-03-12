using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000042 RID: 66
	[XmlInclude(typeof(ServiceConfiguration))]
	[XmlInclude(typeof(BaseRequestType))]
	[XmlInclude(typeof(RuleOperationType))]
	[XmlInclude(typeof(BaseFolderIdType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ExchangeServiceBinding", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(AttendeeConflictData))]
	[XmlInclude(typeof(DirectoryEntryType))]
	[XmlInclude(typeof(BaseResponseMessageType))]
	[XmlInclude(typeof(BaseCalendarItemStateDefinitionType))]
	[XmlInclude(typeof(BaseSubscriptionRequestType))]
	[XmlInclude(typeof(MailboxLocatorType))]
	[XmlInclude(typeof(BaseGroupByType))]
	[XmlInclude(typeof(RecurrenceRangeBaseType))]
	[XmlInclude(typeof(RecurrencePatternBaseType))]
	[XmlInclude(typeof(AttachmentType))]
	[XmlInclude(typeof(ChangeDescriptionType))]
	[XmlInclude(typeof(BasePagingType))]
	[XmlInclude(typeof(BasePermissionType))]
	[XmlInclude(typeof(BaseFolderType))]
	[XmlInclude(typeof(BaseItemIdType))]
	[XmlInclude(typeof(BaseEmailAddressType))]
	public class ExchangeServiceBinding : SoapHttpClientProtocol, IServiceBinding
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000D7D2 File Offset: 0x0000B9D2
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000D7DA File Offset: 0x0000B9DA
		public OpenAsAdminOrSystemServiceType OpenAsAdminOrSystemService { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000D7E3 File Offset: 0x0000B9E3
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000D7EB File Offset: 0x0000B9EB
		public ServiceHttpContext HttpContext { get; set; }

		// Token: 0x060002EF RID: 751 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		protected override XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
		{
			if (this.OpenAsAdminOrSystemService != null)
			{
				if (this.RequestServerVersionValue.Version >= ExchangeVersionType.Exchange2013)
				{
					this.OpenAsAdminOrSystemService.BudgetType = 1;
					this.OpenAsAdminOrSystemService.BudgetTypeSpecified = true;
				}
				message.Headers.Add(this.OpenAsAdminOrSystemService);
			}
			return base.GetWriterForMessage(message, bufferSize);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000D84C File Offset: 0x0000BA4C
		protected override XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
		{
			XmlReader readerForMessage = base.GetReaderForMessage(message, bufferSize);
			XmlTextReader xmlTextReader = readerForMessage as XmlTextReader;
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = false;
				xmlTextReader.DtdProcessing = DtdProcessing.Ignore;
				xmlTextReader.XmlResolver = null;
			}
			return readerForMessage;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D884 File Offset: 0x0000BA84
		protected override WebRequest GetWebRequest(Uri url)
		{
			WebRequest webRequest = base.GetWebRequest(url);
			if (this.HttpContext != null)
			{
				this.HttpContext.SetRequestHttpHeaders(webRequest);
			}
			return webRequest;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse webResponse = base.GetWebResponse(request);
			if (this.HttpContext != null)
			{
				this.HttpContext.UpdateContextFromResponse(webResponse);
			}
			return webResponse;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000D8EA File Offset: 0x0000BAEA
		public ExchangeImpersonationType ExchangeImpersonation
		{
			get
			{
				return this.exchangeImpersonationField;
			}
			set
			{
				this.exchangeImpersonationField = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000D8F3 File Offset: 0x0000BAF3
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000D8FB File Offset: 0x0000BAFB
		public MailboxCultureType MailboxCulture
		{
			get
			{
				return this.mailboxCultureField;
			}
			set
			{
				this.mailboxCultureField = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000D904 File Offset: 0x0000BB04
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public RequestServerVersion RequestServerVersionValue
		{
			get
			{
				return this.requestServerVersionValueField;
			}
			set
			{
				this.requestServerVersionValueField = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000D915 File Offset: 0x0000BB15
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000D91D File Offset: 0x0000BB1D
		public ServerVersionInfo ServerVersionInfoValue
		{
			get
			{
				return this.serverVersionInfoValueField;
			}
			set
			{
				this.serverVersionInfoValueField = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000D926 File Offset: 0x0000BB26
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000D92E File Offset: 0x0000BB2E
		public TimeZoneContextType TimeZoneContext
		{
			get
			{
				return this.timeZoneContextField;
			}
			set
			{
				this.timeZoneContextField = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D937 File Offset: 0x0000BB37
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000D93F File Offset: 0x0000BB3F
		public ManagementRoleType ManagementRole
		{
			get
			{
				return this.managementRoleField;
			}
			set
			{
				this.managementRoleField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000D948 File Offset: 0x0000BB48
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000D950 File Offset: 0x0000BB50
		public DateTimePrecisionType DateTimePrecision
		{
			get
			{
				return this.dateTimePrecisionField;
			}
			set
			{
				this.dateTimePrecisionField = value;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000302 RID: 770 RVA: 0x0000D95C File Offset: 0x0000BB5C
		// (remove) Token: 0x06000303 RID: 771 RVA: 0x0000D994 File Offset: 0x0000BB94
		public event ResolveNamesCompletedEventHandler ResolveNamesCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000304 RID: 772 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		// (remove) Token: 0x06000305 RID: 773 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public event ExpandDLCompletedEventHandler ExpandDLCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000306 RID: 774 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		// (remove) Token: 0x06000307 RID: 775 RVA: 0x0000DA74 File Offset: 0x0000BC74
		public event GetServerTimeZonesCompletedEventHandler GetServerTimeZonesCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000308 RID: 776 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		// (remove) Token: 0x06000309 RID: 777 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public event FindFolderCompletedEventHandler FindFolderCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600030A RID: 778 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		// (remove) Token: 0x0600030B RID: 779 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public event FindItemCompletedEventHandler FindItemCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600030C RID: 780 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		// (remove) Token: 0x0600030D RID: 781 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public event GetFolderCompletedEventHandler GetFolderCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600030E RID: 782 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		// (remove) Token: 0x0600030F RID: 783 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public event UploadItemsCompletedEventHandler UploadItemsCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000310 RID: 784 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		// (remove) Token: 0x06000311 RID: 785 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public event ExportItemsCompletedEventHandler ExportItemsCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000312 RID: 786 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		// (remove) Token: 0x06000313 RID: 787 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public event ConvertIdCompletedEventHandler ConvertIdCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000314 RID: 788 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		// (remove) Token: 0x06000315 RID: 789 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public event CreateFolderCompletedEventHandler CreateFolderCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000316 RID: 790 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		// (remove) Token: 0x06000317 RID: 791 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public event CreateFolderPathCompletedEventHandler CreateFolderPathCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000318 RID: 792 RVA: 0x0000DE2C File Offset: 0x0000C02C
		// (remove) Token: 0x06000319 RID: 793 RVA: 0x0000DE64 File Offset: 0x0000C064
		public event DeleteFolderCompletedEventHandler DeleteFolderCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600031A RID: 794 RVA: 0x0000DE9C File Offset: 0x0000C09C
		// (remove) Token: 0x0600031B RID: 795 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public event EmptyFolderCompletedEventHandler EmptyFolderCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600031C RID: 796 RVA: 0x0000DF0C File Offset: 0x0000C10C
		// (remove) Token: 0x0600031D RID: 797 RVA: 0x0000DF44 File Offset: 0x0000C144
		public event UpdateFolderCompletedEventHandler UpdateFolderCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600031E RID: 798 RVA: 0x0000DF7C File Offset: 0x0000C17C
		// (remove) Token: 0x0600031F RID: 799 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public event MoveFolderCompletedEventHandler MoveFolderCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000320 RID: 800 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		// (remove) Token: 0x06000321 RID: 801 RVA: 0x0000E024 File Offset: 0x0000C224
		public event CopyFolderCompletedEventHandler CopyFolderCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000322 RID: 802 RVA: 0x0000E05C File Offset: 0x0000C25C
		// (remove) Token: 0x06000323 RID: 803 RVA: 0x0000E094 File Offset: 0x0000C294
		public event SubscribeCompletedEventHandler SubscribeCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000324 RID: 804 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		// (remove) Token: 0x06000325 RID: 805 RVA: 0x0000E104 File Offset: 0x0000C304
		public event UnsubscribeCompletedEventHandler UnsubscribeCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000326 RID: 806 RVA: 0x0000E13C File Offset: 0x0000C33C
		// (remove) Token: 0x06000327 RID: 807 RVA: 0x0000E174 File Offset: 0x0000C374
		public event GetEventsCompletedEventHandler GetEventsCompleted;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000328 RID: 808 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		// (remove) Token: 0x06000329 RID: 809 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public event GetStreamingEventsCompletedEventHandler GetStreamingEventsCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600032A RID: 810 RVA: 0x0000E21C File Offset: 0x0000C41C
		// (remove) Token: 0x0600032B RID: 811 RVA: 0x0000E254 File Offset: 0x0000C454
		public event SyncFolderHierarchyCompletedEventHandler SyncFolderHierarchyCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600032C RID: 812 RVA: 0x0000E28C File Offset: 0x0000C48C
		// (remove) Token: 0x0600032D RID: 813 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public event SyncFolderItemsCompletedEventHandler SyncFolderItemsCompleted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600032E RID: 814 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		// (remove) Token: 0x0600032F RID: 815 RVA: 0x0000E334 File Offset: 0x0000C534
		public event CreateManagedFolderCompletedEventHandler CreateManagedFolderCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000330 RID: 816 RVA: 0x0000E36C File Offset: 0x0000C56C
		// (remove) Token: 0x06000331 RID: 817 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public event GetItemCompletedEventHandler GetItemCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000332 RID: 818 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		// (remove) Token: 0x06000333 RID: 819 RVA: 0x0000E414 File Offset: 0x0000C614
		public event CreateItemCompletedEventHandler CreateItemCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000334 RID: 820 RVA: 0x0000E44C File Offset: 0x0000C64C
		// (remove) Token: 0x06000335 RID: 821 RVA: 0x0000E484 File Offset: 0x0000C684
		public event DeleteItemCompletedEventHandler DeleteItemCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000336 RID: 822 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		// (remove) Token: 0x06000337 RID: 823 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public event UpdateItemCompletedEventHandler UpdateItemCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000338 RID: 824 RVA: 0x0000E52C File Offset: 0x0000C72C
		// (remove) Token: 0x06000339 RID: 825 RVA: 0x0000E564 File Offset: 0x0000C764
		public event UpdateItemInRecoverableItemsCompletedEventHandler UpdateItemInRecoverableItemsCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600033A RID: 826 RVA: 0x0000E59C File Offset: 0x0000C79C
		// (remove) Token: 0x0600033B RID: 827 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public event SendItemCompletedEventHandler SendItemCompleted;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600033C RID: 828 RVA: 0x0000E60C File Offset: 0x0000C80C
		// (remove) Token: 0x0600033D RID: 829 RVA: 0x0000E644 File Offset: 0x0000C844
		public event MoveItemCompletedEventHandler MoveItemCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600033E RID: 830 RVA: 0x0000E67C File Offset: 0x0000C87C
		// (remove) Token: 0x0600033F RID: 831 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public event CopyItemCompletedEventHandler CopyItemCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000340 RID: 832 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		// (remove) Token: 0x06000341 RID: 833 RVA: 0x0000E724 File Offset: 0x0000C924
		public event ArchiveItemCompletedEventHandler ArchiveItemCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000342 RID: 834 RVA: 0x0000E75C File Offset: 0x0000C95C
		// (remove) Token: 0x06000343 RID: 835 RVA: 0x0000E794 File Offset: 0x0000C994
		public event CreateAttachmentCompletedEventHandler CreateAttachmentCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000344 RID: 836 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		// (remove) Token: 0x06000345 RID: 837 RVA: 0x0000E804 File Offset: 0x0000CA04
		public event DeleteAttachmentCompletedEventHandler DeleteAttachmentCompleted;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000346 RID: 838 RVA: 0x0000E83C File Offset: 0x0000CA3C
		// (remove) Token: 0x06000347 RID: 839 RVA: 0x0000E874 File Offset: 0x0000CA74
		public event GetAttachmentCompletedEventHandler GetAttachmentCompleted;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000348 RID: 840 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public event GetClientAccessTokenCompletedEventHandler GetClientAccessTokenCompleted;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600034A RID: 842 RVA: 0x0000E91C File Offset: 0x0000CB1C
		// (remove) Token: 0x0600034B RID: 843 RVA: 0x0000E954 File Offset: 0x0000CB54
		public event GetDelegateCompletedEventHandler GetDelegateCompleted;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600034C RID: 844 RVA: 0x0000E98C File Offset: 0x0000CB8C
		// (remove) Token: 0x0600034D RID: 845 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public event AddDelegateCompletedEventHandler AddDelegateCompleted;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600034E RID: 846 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		// (remove) Token: 0x0600034F RID: 847 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public event RemoveDelegateCompletedEventHandler RemoveDelegateCompleted;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000350 RID: 848 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		// (remove) Token: 0x06000351 RID: 849 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public event UpdateDelegateCompletedEventHandler UpdateDelegateCompleted;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000352 RID: 850 RVA: 0x0000EADC File Offset: 0x0000CCDC
		// (remove) Token: 0x06000353 RID: 851 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public event CreateUserConfigurationCompletedEventHandler CreateUserConfigurationCompleted;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000354 RID: 852 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		// (remove) Token: 0x06000355 RID: 853 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public event DeleteUserConfigurationCompletedEventHandler DeleteUserConfigurationCompleted;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000356 RID: 854 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		// (remove) Token: 0x06000357 RID: 855 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		public event GetUserConfigurationCompletedEventHandler GetUserConfigurationCompleted;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000358 RID: 856 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		// (remove) Token: 0x06000359 RID: 857 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public event UpdateUserConfigurationCompletedEventHandler UpdateUserConfigurationCompleted;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600035A RID: 858 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		// (remove) Token: 0x0600035B RID: 859 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public event GetUserAvailabilityCompletedEventHandler GetUserAvailabilityCompleted;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600035C RID: 860 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		// (remove) Token: 0x0600035D RID: 861 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public event GetUserOofSettingsCompletedEventHandler GetUserOofSettingsCompleted;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600035E RID: 862 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		// (remove) Token: 0x0600035F RID: 863 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public event SetUserOofSettingsCompletedEventHandler SetUserOofSettingsCompleted;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000360 RID: 864 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		// (remove) Token: 0x06000361 RID: 865 RVA: 0x0000EE24 File Offset: 0x0000D024
		public event GetServiceConfigurationCompletedEventHandler GetServiceConfigurationCompleted;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000362 RID: 866 RVA: 0x0000EE5C File Offset: 0x0000D05C
		// (remove) Token: 0x06000363 RID: 867 RVA: 0x0000EE94 File Offset: 0x0000D094
		public event GetMailTipsCompletedEventHandler GetMailTipsCompleted;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000364 RID: 868 RVA: 0x0000EECC File Offset: 0x0000D0CC
		// (remove) Token: 0x06000365 RID: 869 RVA: 0x0000EF04 File Offset: 0x0000D104
		public event PlayOnPhoneCompletedEventHandler PlayOnPhoneCompleted;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000366 RID: 870 RVA: 0x0000EF3C File Offset: 0x0000D13C
		// (remove) Token: 0x06000367 RID: 871 RVA: 0x0000EF74 File Offset: 0x0000D174
		public event GetPhoneCallInformationCompletedEventHandler GetPhoneCallInformationCompleted;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000368 RID: 872 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		// (remove) Token: 0x06000369 RID: 873 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public event DisconnectPhoneCallCompletedEventHandler DisconnectPhoneCallCompleted;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600036A RID: 874 RVA: 0x0000F01C File Offset: 0x0000D21C
		// (remove) Token: 0x0600036B RID: 875 RVA: 0x0000F054 File Offset: 0x0000D254
		public event GetSharingMetadataCompletedEventHandler GetSharingMetadataCompleted;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600036C RID: 876 RVA: 0x0000F08C File Offset: 0x0000D28C
		// (remove) Token: 0x0600036D RID: 877 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public event RefreshSharingFolderCompletedEventHandler RefreshSharingFolderCompleted;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600036E RID: 878 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		// (remove) Token: 0x0600036F RID: 879 RVA: 0x0000F134 File Offset: 0x0000D334
		public event GetSharingFolderCompletedEventHandler GetSharingFolderCompleted;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000370 RID: 880 RVA: 0x0000F16C File Offset: 0x0000D36C
		// (remove) Token: 0x06000371 RID: 881 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
		public event SetTeamMailboxCompletedEventHandler SetTeamMailboxCompleted;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000372 RID: 882 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		// (remove) Token: 0x06000373 RID: 883 RVA: 0x0000F214 File Offset: 0x0000D414
		public event UnpinTeamMailboxCompletedEventHandler UnpinTeamMailboxCompleted;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000374 RID: 884 RVA: 0x0000F24C File Offset: 0x0000D44C
		// (remove) Token: 0x06000375 RID: 885 RVA: 0x0000F284 File Offset: 0x0000D484
		public event GetRoomListsCompletedEventHandler GetRoomListsCompleted;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000376 RID: 886 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		// (remove) Token: 0x06000377 RID: 887 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public event GetRoomsCompletedEventHandler GetRoomsCompleted;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000378 RID: 888 RVA: 0x0000F32C File Offset: 0x0000D52C
		// (remove) Token: 0x06000379 RID: 889 RVA: 0x0000F364 File Offset: 0x0000D564
		public event FindMessageTrackingReportCompletedEventHandler FindMessageTrackingReportCompleted;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600037A RID: 890 RVA: 0x0000F39C File Offset: 0x0000D59C
		// (remove) Token: 0x0600037B RID: 891 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public event GetMessageTrackingReportCompletedEventHandler GetMessageTrackingReportCompleted;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600037C RID: 892 RVA: 0x0000F40C File Offset: 0x0000D60C
		// (remove) Token: 0x0600037D RID: 893 RVA: 0x0000F444 File Offset: 0x0000D644
		public event FindConversationCompletedEventHandler FindConversationCompleted;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600037E RID: 894 RVA: 0x0000F47C File Offset: 0x0000D67C
		// (remove) Token: 0x0600037F RID: 895 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		public event ApplyConversationActionCompletedEventHandler ApplyConversationActionCompleted;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000380 RID: 896 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		// (remove) Token: 0x06000381 RID: 897 RVA: 0x0000F524 File Offset: 0x0000D724
		public event GetConversationItemsCompletedEventHandler GetConversationItemsCompleted;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000382 RID: 898 RVA: 0x0000F55C File Offset: 0x0000D75C
		// (remove) Token: 0x06000383 RID: 899 RVA: 0x0000F594 File Offset: 0x0000D794
		public event FindPeopleCompletedEventHandler FindPeopleCompleted;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000384 RID: 900 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		// (remove) Token: 0x06000385 RID: 901 RVA: 0x0000F604 File Offset: 0x0000D804
		public event GetPersonaCompletedEventHandler GetPersonaCompleted;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000386 RID: 902 RVA: 0x0000F63C File Offset: 0x0000D83C
		// (remove) Token: 0x06000387 RID: 903 RVA: 0x0000F674 File Offset: 0x0000D874
		public event GetInboxRulesCompletedEventHandler GetInboxRulesCompleted;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000388 RID: 904 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		// (remove) Token: 0x06000389 RID: 905 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public event UpdateInboxRulesCompletedEventHandler UpdateInboxRulesCompleted;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x0600038A RID: 906 RVA: 0x0000F71C File Offset: 0x0000D91C
		// (remove) Token: 0x0600038B RID: 907 RVA: 0x0000F754 File Offset: 0x0000D954
		public event GetPasswordExpirationDateCompletedEventHandler GetPasswordExpirationDateCompleted;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x0600038C RID: 908 RVA: 0x0000F78C File Offset: 0x0000D98C
		// (remove) Token: 0x0600038D RID: 909 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		public event GetSearchableMailboxesCompletedEventHandler GetSearchableMailboxesCompleted;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600038E RID: 910 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		// (remove) Token: 0x0600038F RID: 911 RVA: 0x0000F834 File Offset: 0x0000DA34
		public event SearchMailboxesCompletedEventHandler SearchMailboxesCompleted;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000390 RID: 912 RVA: 0x0000F86C File Offset: 0x0000DA6C
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public event GetDiscoverySearchConfigurationCompletedEventHandler GetDiscoverySearchConfigurationCompleted;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000392 RID: 914 RVA: 0x0000F8DC File Offset: 0x0000DADC
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x0000F914 File Offset: 0x0000DB14
		public event GetHoldOnMailboxesCompletedEventHandler GetHoldOnMailboxesCompleted;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000394 RID: 916 RVA: 0x0000F94C File Offset: 0x0000DB4C
		// (remove) Token: 0x06000395 RID: 917 RVA: 0x0000F984 File Offset: 0x0000DB84
		public event SetHoldOnMailboxesCompletedEventHandler SetHoldOnMailboxesCompleted;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000396 RID: 918 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		// (remove) Token: 0x06000397 RID: 919 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public event GetNonIndexableItemStatisticsCompletedEventHandler GetNonIndexableItemStatisticsCompleted;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000398 RID: 920 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		// (remove) Token: 0x06000399 RID: 921 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public event GetNonIndexableItemDetailsCompletedEventHandler GetNonIndexableItemDetailsCompleted;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x0600039A RID: 922 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		// (remove) Token: 0x0600039B RID: 923 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public event MarkAllItemsAsReadCompletedEventHandler MarkAllItemsAsReadCompleted;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600039C RID: 924 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		// (remove) Token: 0x0600039D RID: 925 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public event MarkAsJunkCompletedEventHandler MarkAsJunkCompleted;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600039E RID: 926 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		// (remove) Token: 0x0600039F RID: 927 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		public event GetAppManifestsCompletedEventHandler GetAppManifestsCompleted;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060003A0 RID: 928 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		// (remove) Token: 0x060003A1 RID: 929 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public event AddNewImContactToGroupCompletedEventHandler AddNewImContactToGroupCompleted;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060003A2 RID: 930 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		// (remove) Token: 0x060003A3 RID: 931 RVA: 0x0000FC94 File Offset: 0x0000DE94
		public event AddNewTelUriContactToGroupCompletedEventHandler AddNewTelUriContactToGroupCompleted;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060003A4 RID: 932 RVA: 0x0000FCCC File Offset: 0x0000DECC
		// (remove) Token: 0x060003A5 RID: 933 RVA: 0x0000FD04 File Offset: 0x0000DF04
		public event AddImContactToGroupCompletedEventHandler AddImContactToGroupCompleted;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060003A6 RID: 934 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		// (remove) Token: 0x060003A7 RID: 935 RVA: 0x0000FD74 File Offset: 0x0000DF74
		public event RemoveImContactFromGroupCompletedEventHandler RemoveImContactFromGroupCompleted;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060003A8 RID: 936 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		// (remove) Token: 0x060003A9 RID: 937 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public event AddImGroupCompletedEventHandler AddImGroupCompleted;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060003AA RID: 938 RVA: 0x0000FE1C File Offset: 0x0000E01C
		// (remove) Token: 0x060003AB RID: 939 RVA: 0x0000FE54 File Offset: 0x0000E054
		public event AddDistributionGroupToImListCompletedEventHandler AddDistributionGroupToImListCompleted;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060003AC RID: 940 RVA: 0x0000FE8C File Offset: 0x0000E08C
		// (remove) Token: 0x060003AD RID: 941 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public event GetImItemListCompletedEventHandler GetImItemListCompleted;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060003AE RID: 942 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		// (remove) Token: 0x060003AF RID: 943 RVA: 0x0000FF34 File Offset: 0x0000E134
		public event GetImItemsCompletedEventHandler GetImItemsCompleted;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060003B0 RID: 944 RVA: 0x0000FF6C File Offset: 0x0000E16C
		// (remove) Token: 0x060003B1 RID: 945 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public event RemoveContactFromImListCompletedEventHandler RemoveContactFromImListCompleted;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060003B2 RID: 946 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		// (remove) Token: 0x060003B3 RID: 947 RVA: 0x00010014 File Offset: 0x0000E214
		public event RemoveDistributionGroupFromImListCompletedEventHandler RemoveDistributionGroupFromImListCompleted;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060003B4 RID: 948 RVA: 0x0001004C File Offset: 0x0000E24C
		// (remove) Token: 0x060003B5 RID: 949 RVA: 0x00010084 File Offset: 0x0000E284
		public event RemoveImGroupCompletedEventHandler RemoveImGroupCompleted;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060003B6 RID: 950 RVA: 0x000100BC File Offset: 0x0000E2BC
		// (remove) Token: 0x060003B7 RID: 951 RVA: 0x000100F4 File Offset: 0x0000E2F4
		public event SetImGroupCompletedEventHandler SetImGroupCompleted;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060003B8 RID: 952 RVA: 0x0001012C File Offset: 0x0000E32C
		// (remove) Token: 0x060003B9 RID: 953 RVA: 0x00010164 File Offset: 0x0000E364
		public event SetImListMigrationCompletedCompletedEventHandler SetImListMigrationCompletedCompleted;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060003BA RID: 954 RVA: 0x0001019C File Offset: 0x0000E39C
		// (remove) Token: 0x060003BB RID: 955 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public event GetUserRetentionPolicyTagsCompletedEventHandler GetUserRetentionPolicyTagsCompleted;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060003BC RID: 956 RVA: 0x0001020C File Offset: 0x0000E40C
		// (remove) Token: 0x060003BD RID: 957 RVA: 0x00010244 File Offset: 0x0000E444
		public event InstallAppCompletedEventHandler InstallAppCompleted;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060003BE RID: 958 RVA: 0x0001027C File Offset: 0x0000E47C
		// (remove) Token: 0x060003BF RID: 959 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public event UninstallAppCompletedEventHandler UninstallAppCompleted;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060003C0 RID: 960 RVA: 0x000102EC File Offset: 0x0000E4EC
		// (remove) Token: 0x060003C1 RID: 961 RVA: 0x00010324 File Offset: 0x0000E524
		public event DisableAppCompletedEventHandler DisableAppCompleted;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060003C2 RID: 962 RVA: 0x0001035C File Offset: 0x0000E55C
		// (remove) Token: 0x060003C3 RID: 963 RVA: 0x00010394 File Offset: 0x0000E594
		public event GetAppMarketplaceUrlCompletedEventHandler GetAppMarketplaceUrlCompleted;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060003C4 RID: 964 RVA: 0x000103CC File Offset: 0x0000E5CC
		// (remove) Token: 0x060003C5 RID: 965 RVA: 0x00010404 File Offset: 0x0000E604
		public event GetUserPhotoCompletedEventHandler GetUserPhotoCompleted;

		// Token: 0x060003C6 RID: 966 RVA: 0x0001043C File Offset: 0x0000E63C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ResolveNames", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("ResolveNamesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ResolveNamesResponseType ResolveNames([XmlElement("ResolveNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ResolveNamesType ResolveNames1)
		{
			object[] array = base.Invoke("ResolveNames", new object[]
			{
				ResolveNames1
			});
			return (ResolveNamesResponseType)array[0];
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001046C File Offset: 0x0000E66C
		public IAsyncResult BeginResolveNames(ResolveNamesType ResolveNames1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResolveNames", new object[]
			{
				ResolveNames1
			}, callback, asyncState);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010494 File Offset: 0x0000E694
		public ResolveNamesResponseType EndResolveNames(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ResolveNamesResponseType)array[0];
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000104B1 File Offset: 0x0000E6B1
		public void ResolveNamesAsync(ResolveNamesType ResolveNames1)
		{
			this.ResolveNamesAsync(ResolveNames1, null);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000104BC File Offset: 0x0000E6BC
		public void ResolveNamesAsync(ResolveNamesType ResolveNames1, object userState)
		{
			if (this.ResolveNamesOperationCompleted == null)
			{
				this.ResolveNamesOperationCompleted = new SendOrPostCallback(this.OnResolveNamesOperationCompleted);
			}
			base.InvokeAsync("ResolveNames", new object[]
			{
				ResolveNames1
			}, this.ResolveNamesOperationCompleted, userState);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010504 File Offset: 0x0000E704
		private void OnResolveNamesOperationCompleted(object arg)
		{
			if (this.ResolveNamesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResolveNamesCompleted(this, new ResolveNamesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001054C File Offset: 0x0000E74C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExpandDL", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("ExpandDLResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ExpandDLResponseType ExpandDL([XmlElement("ExpandDL", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExpandDLType ExpandDL1)
		{
			object[] array = base.Invoke("ExpandDL", new object[]
			{
				ExpandDL1
			});
			return (ExpandDLResponseType)array[0];
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001057C File Offset: 0x0000E77C
		public IAsyncResult BeginExpandDL(ExpandDLType ExpandDL1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExpandDL", new object[]
			{
				ExpandDL1
			}, callback, asyncState);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public ExpandDLResponseType EndExpandDL(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ExpandDLResponseType)array[0];
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000105C1 File Offset: 0x0000E7C1
		public void ExpandDLAsync(ExpandDLType ExpandDL1)
		{
			this.ExpandDLAsync(ExpandDL1, null);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000105CC File Offset: 0x0000E7CC
		public void ExpandDLAsync(ExpandDLType ExpandDL1, object userState)
		{
			if (this.ExpandDLOperationCompleted == null)
			{
				this.ExpandDLOperationCompleted = new SendOrPostCallback(this.OnExpandDLOperationCompleted);
			}
			base.InvokeAsync("ExpandDL", new object[]
			{
				ExpandDL1
			}, this.ExpandDLOperationCompleted, userState);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00010614 File Offset: 0x0000E814
		private void OnExpandDLOperationCompleted(object arg)
		{
			if (this.ExpandDLCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExpandDLCompleted(this, new ExpandDLCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001065C File Offset: 0x0000E85C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServerTimeZones", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetServerTimeZonesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetServerTimeZonesResponseType GetServerTimeZones([XmlElement("GetServerTimeZones", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServerTimeZonesType GetServerTimeZones1)
		{
			object[] array = base.Invoke("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			});
			return (GetServerTimeZonesResponseType)array[0];
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001068C File Offset: 0x0000E88C
		public IAsyncResult BeginGetServerTimeZones(GetServerTimeZonesType GetServerTimeZones1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			}, callback, asyncState);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public GetServerTimeZonesResponseType EndGetServerTimeZones(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetServerTimeZonesResponseType)array[0];
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000106D1 File Offset: 0x0000E8D1
		public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1)
		{
			this.GetServerTimeZonesAsync(GetServerTimeZones1, null);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000106DC File Offset: 0x0000E8DC
		public void GetServerTimeZonesAsync(GetServerTimeZonesType GetServerTimeZones1, object userState)
		{
			if (this.GetServerTimeZonesOperationCompleted == null)
			{
				this.GetServerTimeZonesOperationCompleted = new SendOrPostCallback(this.OnGetServerTimeZonesOperationCompleted);
			}
			base.InvokeAsync("GetServerTimeZones", new object[]
			{
				GetServerTimeZones1
			}, this.GetServerTimeZonesOperationCompleted, userState);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00010724 File Offset: 0x0000E924
		private void OnGetServerTimeZonesOperationCompleted(object arg)
		{
			if (this.GetServerTimeZonesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServerTimeZonesCompleted(this, new GetServerTimeZonesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001076C File Offset: 0x0000E96C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ManagementRole")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("FindFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindFolderResponseType FindFolder([XmlElement("FindFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindFolderType FindFolder1)
		{
			object[] array = base.Invoke("FindFolder", new object[]
			{
				FindFolder1
			});
			return (FindFolderResponseType)array[0];
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001079C File Offset: 0x0000E99C
		public IAsyncResult BeginFindFolder(FindFolderType FindFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindFolder", new object[]
			{
				FindFolder1
			}, callback, asyncState);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public FindFolderResponseType EndFindFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindFolderResponseType)array[0];
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000107E1 File Offset: 0x0000E9E1
		public void FindFolderAsync(FindFolderType FindFolder1)
		{
			this.FindFolderAsync(FindFolder1, null);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000107EC File Offset: 0x0000E9EC
		public void FindFolderAsync(FindFolderType FindFolder1, object userState)
		{
			if (this.FindFolderOperationCompleted == null)
			{
				this.FindFolderOperationCompleted = new SendOrPostCallback(this.OnFindFolderOperationCompleted);
			}
			base.InvokeAsync("FindFolder", new object[]
			{
				FindFolder1
			}, this.FindFolderOperationCompleted, userState);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010834 File Offset: 0x0000EA34
		private void OnFindFolderOperationCompleted(object arg)
		{
			if (this.FindFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindFolderCompleted(this, new FindFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001087C File Offset: 0x0000EA7C
		[SoapHeader("ManagementRole")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("DateTimePrecision")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("FindItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindItemResponseType FindItem([XmlElement("FindItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindItemType FindItem1)
		{
			object[] array = base.Invoke("FindItem", new object[]
			{
				FindItem1
			});
			return (FindItemResponseType)array[0];
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000108AC File Offset: 0x0000EAAC
		public IAsyncResult BeginFindItem(FindItemType FindItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindItem", new object[]
			{
				FindItem1
			}, callback, asyncState);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000108D4 File Offset: 0x0000EAD4
		public FindItemResponseType EndFindItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindItemResponseType)array[0];
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000108F1 File Offset: 0x0000EAF1
		public void FindItemAsync(FindItemType FindItem1)
		{
			this.FindItemAsync(FindItem1, null);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000108FC File Offset: 0x0000EAFC
		public void FindItemAsync(FindItemType FindItem1, object userState)
		{
			if (this.FindItemOperationCompleted == null)
			{
				this.FindItemOperationCompleted = new SendOrPostCallback(this.OnFindItemOperationCompleted);
			}
			base.InvokeAsync("FindItem", new object[]
			{
				FindItem1
			}, this.FindItemOperationCompleted, userState);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010944 File Offset: 0x0000EB44
		private void OnFindItemOperationCompleted(object arg)
		{
			if (this.FindItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindItemCompleted(this, new FindItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001098C File Offset: 0x0000EB8C
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ManagementRole")]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("GetFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetFolderResponseType GetFolder([XmlElement("GetFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetFolderType GetFolder1)
		{
			object[] array = base.Invoke("GetFolder", new object[]
			{
				GetFolder1
			});
			return (GetFolderResponseType)array[0];
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000109BC File Offset: 0x0000EBBC
		public IAsyncResult BeginGetFolder(GetFolderType GetFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFolder", new object[]
			{
				GetFolder1
			}, callback, asyncState);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000109E4 File Offset: 0x0000EBE4
		public GetFolderResponseType EndGetFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetFolderResponseType)array[0];
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00010A01 File Offset: 0x0000EC01
		public void GetFolderAsync(GetFolderType GetFolder1)
		{
			this.GetFolderAsync(GetFolder1, null);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public void GetFolderAsync(GetFolderType GetFolder1, object userState)
		{
			if (this.GetFolderOperationCompleted == null)
			{
				this.GetFolderOperationCompleted = new SendOrPostCallback(this.OnGetFolderOperationCompleted);
			}
			base.InvokeAsync("GetFolder", new object[]
			{
				GetFolder1
			}, this.GetFolderOperationCompleted, userState);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010A54 File Offset: 0x0000EC54
		private void OnGetFolderOperationCompleted(object arg)
		{
			if (this.GetFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFolderCompleted(this, new GetFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010A9C File Offset: 0x0000EC9C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UploadItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("UploadItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UploadItemsResponseType UploadItems([XmlElement("UploadItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UploadItemsType UploadItems1)
		{
			object[] array = base.Invoke("UploadItems", new object[]
			{
				UploadItems1
			});
			return (UploadItemsResponseType)array[0];
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010ACC File Offset: 0x0000ECCC
		public IAsyncResult BeginUploadItems(UploadItemsType UploadItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UploadItems", new object[]
			{
				UploadItems1
			}, callback, asyncState);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		public UploadItemsResponseType EndUploadItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UploadItemsResponseType)array[0];
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010B11 File Offset: 0x0000ED11
		public void UploadItemsAsync(UploadItemsType UploadItems1)
		{
			this.UploadItemsAsync(UploadItems1, null);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public void UploadItemsAsync(UploadItemsType UploadItems1, object userState)
		{
			if (this.UploadItemsOperationCompleted == null)
			{
				this.UploadItemsOperationCompleted = new SendOrPostCallback(this.OnUploadItemsOperationCompleted);
			}
			base.InvokeAsync("UploadItems", new object[]
			{
				UploadItems1
			}, this.UploadItemsOperationCompleted, userState);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010B64 File Offset: 0x0000ED64
		private void OnUploadItemsOperationCompleted(object arg)
		{
			if (this.UploadItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UploadItemsCompleted(this, new UploadItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010BAC File Offset: 0x0000EDAC
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ExportItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("ExportItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ExportItemsResponseType ExportItems([XmlElement("ExportItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ExportItemsType ExportItems1)
		{
			object[] array = base.Invoke("ExportItems", new object[]
			{
				ExportItems1
			});
			return (ExportItemsResponseType)array[0];
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public IAsyncResult BeginExportItems(ExportItemsType ExportItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportItems", new object[]
			{
				ExportItems1
			}, callback, asyncState);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010C04 File Offset: 0x0000EE04
		public ExportItemsResponseType EndExportItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ExportItemsResponseType)array[0];
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010C21 File Offset: 0x0000EE21
		public void ExportItemsAsync(ExportItemsType ExportItems1)
		{
			this.ExportItemsAsync(ExportItems1, null);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public void ExportItemsAsync(ExportItemsType ExportItems1, object userState)
		{
			if (this.ExportItemsOperationCompleted == null)
			{
				this.ExportItemsOperationCompleted = new SendOrPostCallback(this.OnExportItemsOperationCompleted);
			}
			base.InvokeAsync("ExportItems", new object[]
			{
				ExportItems1
			}, this.ExportItemsOperationCompleted, userState);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010C74 File Offset: 0x0000EE74
		private void OnExportItemsOperationCompleted(object arg)
		{
			if (this.ExportItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportItemsCompleted(this, new ExportItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010CBC File Offset: 0x0000EEBC
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ConvertId", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("ConvertIdResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ConvertIdResponseType ConvertId([XmlElement("ConvertId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ConvertIdType ConvertId1)
		{
			object[] array = base.Invoke("ConvertId", new object[]
			{
				ConvertId1
			});
			return (ConvertIdResponseType)array[0];
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010CEC File Offset: 0x0000EEEC
		public IAsyncResult BeginConvertId(ConvertIdType ConvertId1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ConvertId", new object[]
			{
				ConvertId1
			}, callback, asyncState);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010D14 File Offset: 0x0000EF14
		public ConvertIdResponseType EndConvertId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ConvertIdResponseType)array[0];
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010D31 File Offset: 0x0000EF31
		public void ConvertIdAsync(ConvertIdType ConvertId1)
		{
			this.ConvertIdAsync(ConvertId1, null);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010D3C File Offset: 0x0000EF3C
		public void ConvertIdAsync(ConvertIdType ConvertId1, object userState)
		{
			if (this.ConvertIdOperationCompleted == null)
			{
				this.ConvertIdOperationCompleted = new SendOrPostCallback(this.OnConvertIdOperationCompleted);
			}
			base.InvokeAsync("ConvertId", new object[]
			{
				ConvertId1
			}, this.ConvertIdOperationCompleted, userState);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010D84 File Offset: 0x0000EF84
		private void OnConvertIdOperationCompleted(object arg)
		{
			if (this.ConvertIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ConvertIdCompleted(this, new ConvertIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010DCC File Offset: 0x0000EFCC
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("CreateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateFolderResponseType CreateFolder([XmlElement("CreateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderType CreateFolder1)
		{
			object[] array = base.Invoke("CreateFolder", new object[]
			{
				CreateFolder1
			});
			return (CreateFolderResponseType)array[0];
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public IAsyncResult BeginCreateFolder(CreateFolderType CreateFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolder", new object[]
			{
				CreateFolder1
			}, callback, asyncState);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010E24 File Offset: 0x0000F024
		public CreateFolderResponseType EndCreateFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateFolderResponseType)array[0];
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010E41 File Offset: 0x0000F041
		public void CreateFolderAsync(CreateFolderType CreateFolder1)
		{
			this.CreateFolderAsync(CreateFolder1, null);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010E4C File Offset: 0x0000F04C
		public void CreateFolderAsync(CreateFolderType CreateFolder1, object userState)
		{
			if (this.CreateFolderOperationCompleted == null)
			{
				this.CreateFolderOperationCompleted = new SendOrPostCallback(this.OnCreateFolderOperationCompleted);
			}
			base.InvokeAsync("CreateFolder", new object[]
			{
				CreateFolder1
			}, this.CreateFolderOperationCompleted, userState);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010E94 File Offset: 0x0000F094
		private void OnCreateFolderOperationCompleted(object arg)
		{
			if (this.CreateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderCompleted(this, new CreateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010EDC File Offset: 0x0000F0DC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateFolderPath", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("CreateFolderPathResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateFolderPathResponseType CreateFolderPath([XmlElement("CreateFolderPath", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateFolderPathType CreateFolderPath1)
		{
			object[] array = base.Invoke("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			});
			return (CreateFolderPathResponseType)array[0];
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010F0C File Offset: 0x0000F10C
		public IAsyncResult BeginCreateFolderPath(CreateFolderPathType CreateFolderPath1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			}, callback, asyncState);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010F34 File Offset: 0x0000F134
		public CreateFolderPathResponseType EndCreateFolderPath(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateFolderPathResponseType)array[0];
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010F51 File Offset: 0x0000F151
		public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1)
		{
			this.CreateFolderPathAsync(CreateFolderPath1, null);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010F5C File Offset: 0x0000F15C
		public void CreateFolderPathAsync(CreateFolderPathType CreateFolderPath1, object userState)
		{
			if (this.CreateFolderPathOperationCompleted == null)
			{
				this.CreateFolderPathOperationCompleted = new SendOrPostCallback(this.OnCreateFolderPathOperationCompleted);
			}
			base.InvokeAsync("CreateFolderPath", new object[]
			{
				CreateFolderPath1
			}, this.CreateFolderPathOperationCompleted, userState);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		private void OnCreateFolderPathOperationCompleted(object arg)
		{
			if (this.CreateFolderPathCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateFolderPathCompleted(this, new CreateFolderPathCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010FEC File Offset: 0x0000F1EC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("DeleteFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteFolderResponseType DeleteFolder([XmlElement("DeleteFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteFolderType DeleteFolder1)
		{
			object[] array = base.Invoke("DeleteFolder", new object[]
			{
				DeleteFolder1
			});
			return (DeleteFolderResponseType)array[0];
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001101C File Offset: 0x0000F21C
		public IAsyncResult BeginDeleteFolder(DeleteFolderType DeleteFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteFolder", new object[]
			{
				DeleteFolder1
			}, callback, asyncState);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011044 File Offset: 0x0000F244
		public DeleteFolderResponseType EndDeleteFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteFolderResponseType)array[0];
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011061 File Offset: 0x0000F261
		public void DeleteFolderAsync(DeleteFolderType DeleteFolder1)
		{
			this.DeleteFolderAsync(DeleteFolder1, null);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001106C File Offset: 0x0000F26C
		public void DeleteFolderAsync(DeleteFolderType DeleteFolder1, object userState)
		{
			if (this.DeleteFolderOperationCompleted == null)
			{
				this.DeleteFolderOperationCompleted = new SendOrPostCallback(this.OnDeleteFolderOperationCompleted);
			}
			base.InvokeAsync("DeleteFolder", new object[]
			{
				DeleteFolder1
			}, this.DeleteFolderOperationCompleted, userState);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000110B4 File Offset: 0x0000F2B4
		private void OnDeleteFolderOperationCompleted(object arg)
		{
			if (this.DeleteFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteFolderCompleted(this, new DeleteFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000110FC File Offset: 0x0000F2FC
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/EmptyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("EmptyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EmptyFolderResponseType EmptyFolder([XmlElement("EmptyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] EmptyFolderType EmptyFolder1)
		{
			object[] array = base.Invoke("EmptyFolder", new object[]
			{
				EmptyFolder1
			});
			return (EmptyFolderResponseType)array[0];
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001112C File Offset: 0x0000F32C
		public IAsyncResult BeginEmptyFolder(EmptyFolderType EmptyFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EmptyFolder", new object[]
			{
				EmptyFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011154 File Offset: 0x0000F354
		public EmptyFolderResponseType EndEmptyFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EmptyFolderResponseType)array[0];
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011171 File Offset: 0x0000F371
		public void EmptyFolderAsync(EmptyFolderType EmptyFolder1)
		{
			this.EmptyFolderAsync(EmptyFolder1, null);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001117C File Offset: 0x0000F37C
		public void EmptyFolderAsync(EmptyFolderType EmptyFolder1, object userState)
		{
			if (this.EmptyFolderOperationCompleted == null)
			{
				this.EmptyFolderOperationCompleted = new SendOrPostCallback(this.OnEmptyFolderOperationCompleted);
			}
			base.InvokeAsync("EmptyFolder", new object[]
			{
				EmptyFolder1
			}, this.EmptyFolderOperationCompleted, userState);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000111C4 File Offset: 0x0000F3C4
		private void OnEmptyFolderOperationCompleted(object arg)
		{
			if (this.EmptyFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EmptyFolderCompleted(this, new EmptyFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001120C File Offset: 0x0000F40C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("UpdateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateFolderResponseType UpdateFolder([XmlElement("UpdateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateFolderType UpdateFolder1)
		{
			object[] array = base.Invoke("UpdateFolder", new object[]
			{
				UpdateFolder1
			});
			return (UpdateFolderResponseType)array[0];
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001123C File Offset: 0x0000F43C
		public IAsyncResult BeginUpdateFolder(UpdateFolderType UpdateFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateFolder", new object[]
			{
				UpdateFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011264 File Offset: 0x0000F464
		public UpdateFolderResponseType EndUpdateFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateFolderResponseType)array[0];
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011281 File Offset: 0x0000F481
		public void UpdateFolderAsync(UpdateFolderType UpdateFolder1)
		{
			this.UpdateFolderAsync(UpdateFolder1, null);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001128C File Offset: 0x0000F48C
		public void UpdateFolderAsync(UpdateFolderType UpdateFolder1, object userState)
		{
			if (this.UpdateFolderOperationCompleted == null)
			{
				this.UpdateFolderOperationCompleted = new SendOrPostCallback(this.OnUpdateFolderOperationCompleted);
			}
			base.InvokeAsync("UpdateFolder", new object[]
			{
				UpdateFolder1
			}, this.UpdateFolderOperationCompleted, userState);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000112D4 File Offset: 0x0000F4D4
		private void OnUpdateFolderOperationCompleted(object arg)
		{
			if (this.UpdateFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateFolderCompleted(this, new UpdateFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001131C File Offset: 0x0000F51C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("MoveFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MoveFolderResponseType MoveFolder([XmlElement("MoveFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveFolderType MoveFolder1)
		{
			object[] array = base.Invoke("MoveFolder", new object[]
			{
				MoveFolder1
			});
			return (MoveFolderResponseType)array[0];
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001134C File Offset: 0x0000F54C
		public IAsyncResult BeginMoveFolder(MoveFolderType MoveFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveFolder", new object[]
			{
				MoveFolder1
			}, callback, asyncState);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011374 File Offset: 0x0000F574
		public MoveFolderResponseType EndMoveFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MoveFolderResponseType)array[0];
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011391 File Offset: 0x0000F591
		public void MoveFolderAsync(MoveFolderType MoveFolder1)
		{
			this.MoveFolderAsync(MoveFolder1, null);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001139C File Offset: 0x0000F59C
		public void MoveFolderAsync(MoveFolderType MoveFolder1, object userState)
		{
			if (this.MoveFolderOperationCompleted == null)
			{
				this.MoveFolderOperationCompleted = new SendOrPostCallback(this.OnMoveFolderOperationCompleted);
			}
			base.InvokeAsync("MoveFolder", new object[]
			{
				MoveFolder1
			}, this.MoveFolderOperationCompleted, userState);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000113E4 File Offset: 0x0000F5E4
		private void OnMoveFolderOperationCompleted(object arg)
		{
			if (this.MoveFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveFolderCompleted(this, new MoveFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001142C File Offset: 0x0000F62C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("CopyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CopyFolderResponseType CopyFolder([XmlElement("CopyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyFolderType CopyFolder1)
		{
			object[] array = base.Invoke("CopyFolder", new object[]
			{
				CopyFolder1
			});
			return (CopyFolderResponseType)array[0];
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001145C File Offset: 0x0000F65C
		public IAsyncResult BeginCopyFolder(CopyFolderType CopyFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CopyFolder", new object[]
			{
				CopyFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011484 File Offset: 0x0000F684
		public CopyFolderResponseType EndCopyFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CopyFolderResponseType)array[0];
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000114A1 File Offset: 0x0000F6A1
		public void CopyFolderAsync(CopyFolderType CopyFolder1)
		{
			this.CopyFolderAsync(CopyFolder1, null);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000114AC File Offset: 0x0000F6AC
		public void CopyFolderAsync(CopyFolderType CopyFolder1, object userState)
		{
			if (this.CopyFolderOperationCompleted == null)
			{
				this.CopyFolderOperationCompleted = new SendOrPostCallback(this.OnCopyFolderOperationCompleted);
			}
			base.InvokeAsync("CopyFolder", new object[]
			{
				CopyFolder1
			}, this.CopyFolderOperationCompleted, userState);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000114F4 File Offset: 0x0000F6F4
		private void OnCopyFolderOperationCompleted(object arg)
		{
			if (this.CopyFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CopyFolderCompleted(this, new CopyFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001153C File Offset: 0x0000F73C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Subscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SubscribeResponseType Subscribe([XmlElement("Subscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SubscribeType Subscribe1)
		{
			object[] array = base.Invoke("Subscribe", new object[]
			{
				Subscribe1
			});
			return (SubscribeResponseType)array[0];
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001156C File Offset: 0x0000F76C
		public IAsyncResult BeginSubscribe(SubscribeType Subscribe1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Subscribe", new object[]
			{
				Subscribe1
			}, callback, asyncState);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011594 File Offset: 0x0000F794
		public SubscribeResponseType EndSubscribe(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SubscribeResponseType)array[0];
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000115B1 File Offset: 0x0000F7B1
		public void SubscribeAsync(SubscribeType Subscribe1)
		{
			this.SubscribeAsync(Subscribe1, null);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000115BC File Offset: 0x0000F7BC
		public void SubscribeAsync(SubscribeType Subscribe1, object userState)
		{
			if (this.SubscribeOperationCompleted == null)
			{
				this.SubscribeOperationCompleted = new SendOrPostCallback(this.OnSubscribeOperationCompleted);
			}
			base.InvokeAsync("Subscribe", new object[]
			{
				Subscribe1
			}, this.SubscribeOperationCompleted, userState);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011604 File Offset: 0x0000F804
		private void OnSubscribeOperationCompleted(object arg)
		{
			if (this.SubscribeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SubscribeCompleted(this, new SubscribeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001164C File Offset: 0x0000F84C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/Unsubscribe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UnsubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UnsubscribeResponseType Unsubscribe([XmlElement("Unsubscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnsubscribeType Unsubscribe1)
		{
			object[] array = base.Invoke("Unsubscribe", new object[]
			{
				Unsubscribe1
			});
			return (UnsubscribeResponseType)array[0];
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001167C File Offset: 0x0000F87C
		public IAsyncResult BeginUnsubscribe(UnsubscribeType Unsubscribe1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Unsubscribe", new object[]
			{
				Unsubscribe1
			}, callback, asyncState);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public UnsubscribeResponseType EndUnsubscribe(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UnsubscribeResponseType)array[0];
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public void UnsubscribeAsync(UnsubscribeType Unsubscribe1)
		{
			this.UnsubscribeAsync(Unsubscribe1, null);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000116CC File Offset: 0x0000F8CC
		public void UnsubscribeAsync(UnsubscribeType Unsubscribe1, object userState)
		{
			if (this.UnsubscribeOperationCompleted == null)
			{
				this.UnsubscribeOperationCompleted = new SendOrPostCallback(this.OnUnsubscribeOperationCompleted);
			}
			base.InvokeAsync("Unsubscribe", new object[]
			{
				Unsubscribe1
			}, this.UnsubscribeOperationCompleted, userState);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00011714 File Offset: 0x0000F914
		private void OnUnsubscribeOperationCompleted(object arg)
		{
			if (this.UnsubscribeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UnsubscribeCompleted(this, new UnsubscribeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001175C File Offset: 0x0000F95C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("GetEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetEventsResponseType GetEvents([XmlElement("GetEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetEventsType GetEvents1)
		{
			object[] array = base.Invoke("GetEvents", new object[]
			{
				GetEvents1
			});
			return (GetEventsResponseType)array[0];
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001178C File Offset: 0x0000F98C
		public IAsyncResult BeginGetEvents(GetEventsType GetEvents1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetEvents", new object[]
			{
				GetEvents1
			}, callback, asyncState);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public GetEventsResponseType EndGetEvents(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetEventsResponseType)array[0];
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000117D1 File Offset: 0x0000F9D1
		public void GetEventsAsync(GetEventsType GetEvents1)
		{
			this.GetEventsAsync(GetEvents1, null);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000117DC File Offset: 0x0000F9DC
		public void GetEventsAsync(GetEventsType GetEvents1, object userState)
		{
			if (this.GetEventsOperationCompleted == null)
			{
				this.GetEventsOperationCompleted = new SendOrPostCallback(this.OnGetEventsOperationCompleted);
			}
			base.InvokeAsync("GetEvents", new object[]
			{
				GetEvents1
			}, this.GetEventsOperationCompleted, userState);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011824 File Offset: 0x0000FA24
		private void OnGetEventsOperationCompleted(object arg)
		{
			if (this.GetEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetEventsCompleted(this, new GetEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001186C File Offset: 0x0000FA6C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetEvents", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetStreamingEventsResponseType GetStreamingEvents([XmlElement("GetStreamingEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetStreamingEventsType GetStreamingEvents1)
		{
			object[] array = base.Invoke("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			});
			return (GetStreamingEventsResponseType)array[0];
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001189C File Offset: 0x0000FA9C
		public IAsyncResult BeginGetStreamingEvents(GetStreamingEventsType GetStreamingEvents1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			}, callback, asyncState);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public GetStreamingEventsResponseType EndGetStreamingEvents(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetStreamingEventsResponseType)array[0];
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000118E1 File Offset: 0x0000FAE1
		public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1)
		{
			this.GetStreamingEventsAsync(GetStreamingEvents1, null);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000118EC File Offset: 0x0000FAEC
		public void GetStreamingEventsAsync(GetStreamingEventsType GetStreamingEvents1, object userState)
		{
			if (this.GetStreamingEventsOperationCompleted == null)
			{
				this.GetStreamingEventsOperationCompleted = new SendOrPostCallback(this.OnGetStreamingEventsOperationCompleted);
			}
			base.InvokeAsync("GetStreamingEvents", new object[]
			{
				GetStreamingEvents1
			}, this.GetStreamingEventsOperationCompleted, userState);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011934 File Offset: 0x0000FB34
		private void OnGetStreamingEventsOperationCompleted(object arg)
		{
			if (this.GetStreamingEventsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetStreamingEventsCompleted(this, new GetStreamingEventsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001197C File Offset: 0x0000FB7C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderHierarchy", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("SyncFolderHierarchyResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SyncFolderHierarchyResponseType SyncFolderHierarchy([XmlElement("SyncFolderHierarchy", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderHierarchyType SyncFolderHierarchy1)
		{
			object[] array = base.Invoke("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			});
			return (SyncFolderHierarchyResponseType)array[0];
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000119AC File Offset: 0x0000FBAC
		public IAsyncResult BeginSyncFolderHierarchy(SyncFolderHierarchyType SyncFolderHierarchy1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			}, callback, asyncState);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public SyncFolderHierarchyResponseType EndSyncFolderHierarchy(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SyncFolderHierarchyResponseType)array[0];
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000119F1 File Offset: 0x0000FBF1
		public void SyncFolderHierarchyAsync(SyncFolderHierarchyType SyncFolderHierarchy1)
		{
			this.SyncFolderHierarchyAsync(SyncFolderHierarchy1, null);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000119FC File Offset: 0x0000FBFC
		public void SyncFolderHierarchyAsync(SyncFolderHierarchyType SyncFolderHierarchy1, object userState)
		{
			if (this.SyncFolderHierarchyOperationCompleted == null)
			{
				this.SyncFolderHierarchyOperationCompleted = new SendOrPostCallback(this.OnSyncFolderHierarchyOperationCompleted);
			}
			base.InvokeAsync("SyncFolderHierarchy", new object[]
			{
				SyncFolderHierarchy1
			}, this.SyncFolderHierarchyOperationCompleted, userState);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011A44 File Offset: 0x0000FC44
		private void OnSyncFolderHierarchyOperationCompleted(object arg)
		{
			if (this.SyncFolderHierarchyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SyncFolderHierarchyCompleted(this, new SyncFolderHierarchyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011A8C File Offset: 0x0000FC8C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SyncFolderItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("SyncFolderItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SyncFolderItemsResponseType SyncFolderItems([XmlElement("SyncFolderItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SyncFolderItemsType SyncFolderItems1)
		{
			object[] array = base.Invoke("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			});
			return (SyncFolderItemsResponseType)array[0];
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011ABC File Offset: 0x0000FCBC
		public IAsyncResult BeginSyncFolderItems(SyncFolderItemsType SyncFolderItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			}, callback, asyncState);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public SyncFolderItemsResponseType EndSyncFolderItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SyncFolderItemsResponseType)array[0];
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011B01 File Offset: 0x0000FD01
		public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1)
		{
			this.SyncFolderItemsAsync(SyncFolderItems1, null);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public void SyncFolderItemsAsync(SyncFolderItemsType SyncFolderItems1, object userState)
		{
			if (this.SyncFolderItemsOperationCompleted == null)
			{
				this.SyncFolderItemsOperationCompleted = new SendOrPostCallback(this.OnSyncFolderItemsOperationCompleted);
			}
			base.InvokeAsync("SyncFolderItems", new object[]
			{
				SyncFolderItems1
			}, this.SyncFolderItemsOperationCompleted, userState);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00011B54 File Offset: 0x0000FD54
		private void OnSyncFolderItemsOperationCompleted(object arg)
		{
			if (this.SyncFolderItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SyncFolderItemsCompleted(this, new SyncFolderItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00011B9C File Offset: 0x0000FD9C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateManagedFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("CreateManagedFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateManagedFolderResponseType CreateManagedFolder([XmlElement("CreateManagedFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateManagedFolderRequestType CreateManagedFolder1)
		{
			object[] array = base.Invoke("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			});
			return (CreateManagedFolderResponseType)array[0];
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public IAsyncResult BeginCreateManagedFolder(CreateManagedFolderRequestType CreateManagedFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			}, callback, asyncState);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public CreateManagedFolderResponseType EndCreateManagedFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateManagedFolderResponseType)array[0];
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00011C11 File Offset: 0x0000FE11
		public void CreateManagedFolderAsync(CreateManagedFolderRequestType CreateManagedFolder1)
		{
			this.CreateManagedFolderAsync(CreateManagedFolder1, null);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public void CreateManagedFolderAsync(CreateManagedFolderRequestType CreateManagedFolder1, object userState)
		{
			if (this.CreateManagedFolderOperationCompleted == null)
			{
				this.CreateManagedFolderOperationCompleted = new SendOrPostCallback(this.OnCreateManagedFolderOperationCompleted);
			}
			base.InvokeAsync("CreateManagedFolder", new object[]
			{
				CreateManagedFolder1
			}, this.CreateManagedFolderOperationCompleted, userState);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011C64 File Offset: 0x0000FE64
		private void OnCreateManagedFolderOperationCompleted(object arg)
		{
			if (this.CreateManagedFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateManagedFolderCompleted(this, new CreateManagedFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011CAC File Offset: 0x0000FEAC
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("DateTimePrecision")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ManagementRole")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("GetItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetItemResponseType GetItem([XmlElement("GetItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetItemType GetItem1)
		{
			object[] array = base.Invoke("GetItem", new object[]
			{
				GetItem1
			});
			return (GetItemResponseType)array[0];
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public IAsyncResult BeginGetItem(GetItemType GetItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetItem", new object[]
			{
				GetItem1
			}, callback, asyncState);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00011D04 File Offset: 0x0000FF04
		public GetItemResponseType EndGetItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetItemResponseType)array[0];
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011D21 File Offset: 0x0000FF21
		public void GetItemAsync(GetItemType GetItem1)
		{
			this.GetItemAsync(GetItem1, null);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public void GetItemAsync(GetItemType GetItem1, object userState)
		{
			if (this.GetItemOperationCompleted == null)
			{
				this.GetItemOperationCompleted = new SendOrPostCallback(this.OnGetItemOperationCompleted);
			}
			base.InvokeAsync("GetItem", new object[]
			{
				GetItem1
			}, this.GetItemOperationCompleted, userState);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00011D74 File Offset: 0x0000FF74
		private void OnGetItemOperationCompleted(object arg)
		{
			if (this.GetItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetItemCompleted(this, new GetItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00011DBC File Offset: 0x0000FFBC
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateItemResponseType CreateItem([XmlElement("CreateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateItemType CreateItem1)
		{
			object[] array = base.Invoke("CreateItem", new object[]
			{
				CreateItem1
			});
			return (CreateItemResponseType)array[0];
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00011DEC File Offset: 0x0000FFEC
		public IAsyncResult BeginCreateItem(CreateItemType CreateItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateItem", new object[]
			{
				CreateItem1
			}, callback, asyncState);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00011E14 File Offset: 0x00010014
		public CreateItemResponseType EndCreateItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateItemResponseType)array[0];
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00011E31 File Offset: 0x00010031
		public void CreateItemAsync(CreateItemType CreateItem1)
		{
			this.CreateItemAsync(CreateItem1, null);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00011E3C File Offset: 0x0001003C
		public void CreateItemAsync(CreateItemType CreateItem1, object userState)
		{
			if (this.CreateItemOperationCompleted == null)
			{
				this.CreateItemOperationCompleted = new SendOrPostCallback(this.OnCreateItemOperationCompleted);
			}
			base.InvokeAsync("CreateItem", new object[]
			{
				CreateItem1
			}, this.CreateItemOperationCompleted, userState);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00011E84 File Offset: 0x00010084
		private void OnCreateItemOperationCompleted(object arg)
		{
			if (this.CreateItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateItemCompleted(this, new CreateItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00011ECC File Offset: 0x000100CC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("DeleteItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteItemResponseType DeleteItem([XmlElement("DeleteItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteItemType DeleteItem1)
		{
			object[] array = base.Invoke("DeleteItem", new object[]
			{
				DeleteItem1
			});
			return (DeleteItemResponseType)array[0];
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00011EFC File Offset: 0x000100FC
		public IAsyncResult BeginDeleteItem(DeleteItemType DeleteItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteItem", new object[]
			{
				DeleteItem1
			}, callback, asyncState);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00011F24 File Offset: 0x00010124
		public DeleteItemResponseType EndDeleteItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteItemResponseType)array[0];
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00011F41 File Offset: 0x00010141
		public void DeleteItemAsync(DeleteItemType DeleteItem1)
		{
			this.DeleteItemAsync(DeleteItem1, null);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00011F4C File Offset: 0x0001014C
		public void DeleteItemAsync(DeleteItemType DeleteItem1, object userState)
		{
			if (this.DeleteItemOperationCompleted == null)
			{
				this.DeleteItemOperationCompleted = new SendOrPostCallback(this.OnDeleteItemOperationCompleted);
			}
			base.InvokeAsync("DeleteItem", new object[]
			{
				DeleteItem1
			}, this.DeleteItemOperationCompleted, userState);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011F94 File Offset: 0x00010194
		private void OnDeleteItemOperationCompleted(object arg)
		{
			if (this.DeleteItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteItemCompleted(this, new DeleteItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00011FDC File Offset: 0x000101DC
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("UpdateItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateItemResponseType UpdateItem([XmlElement("UpdateItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemType UpdateItem1)
		{
			object[] array = base.Invoke("UpdateItem", new object[]
			{
				UpdateItem1
			});
			return (UpdateItemResponseType)array[0];
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001200C File Offset: 0x0001020C
		public IAsyncResult BeginUpdateItem(UpdateItemType UpdateItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateItem", new object[]
			{
				UpdateItem1
			}, callback, asyncState);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00012034 File Offset: 0x00010234
		public UpdateItemResponseType EndUpdateItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateItemResponseType)array[0];
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012051 File Offset: 0x00010251
		public void UpdateItemAsync(UpdateItemType UpdateItem1)
		{
			this.UpdateItemAsync(UpdateItem1, null);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001205C File Offset: 0x0001025C
		public void UpdateItemAsync(UpdateItemType UpdateItem1, object userState)
		{
			if (this.UpdateItemOperationCompleted == null)
			{
				this.UpdateItemOperationCompleted = new SendOrPostCallback(this.OnUpdateItemOperationCompleted);
			}
			base.InvokeAsync("UpdateItem", new object[]
			{
				UpdateItem1
			}, this.UpdateItemOperationCompleted, userState);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000120A4 File Offset: 0x000102A4
		private void OnUpdateItemOperationCompleted(object arg)
		{
			if (this.UpdateItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateItemCompleted(this, new UpdateItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000120EC File Offset: 0x000102EC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateItemInRecoverableItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("UpdateItemInRecoverableItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateItemInRecoverableItemsResponseType UpdateItemInRecoverableItems([XmlElement("UpdateItemInRecoverableItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
		{
			object[] array = base.Invoke("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			});
			return (UpdateItemInRecoverableItemsResponseType)array[0];
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001211C File Offset: 0x0001031C
		public IAsyncResult BeginUpdateItemInRecoverableItems(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			}, callback, asyncState);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012144 File Offset: 0x00010344
		public UpdateItemInRecoverableItemsResponseType EndUpdateItemInRecoverableItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateItemInRecoverableItemsResponseType)array[0];
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012161 File Offset: 0x00010361
		public void UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1)
		{
			this.UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItems1, null);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001216C File Offset: 0x0001036C
		public void UpdateItemInRecoverableItemsAsync(UpdateItemInRecoverableItemsType UpdateItemInRecoverableItems1, object userState)
		{
			if (this.UpdateItemInRecoverableItemsOperationCompleted == null)
			{
				this.UpdateItemInRecoverableItemsOperationCompleted = new SendOrPostCallback(this.OnUpdateItemInRecoverableItemsOperationCompleted);
			}
			base.InvokeAsync("UpdateItemInRecoverableItems", new object[]
			{
				UpdateItemInRecoverableItems1
			}, this.UpdateItemInRecoverableItemsOperationCompleted, userState);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000121B4 File Offset: 0x000103B4
		private void OnUpdateItemInRecoverableItemsOperationCompleted(object arg)
		{
			if (this.UpdateItemInRecoverableItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateItemInRecoverableItemsCompleted(this, new UpdateItemInRecoverableItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000121FC File Offset: 0x000103FC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SendItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("SendItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SendItemResponseType SendItem([XmlElement("SendItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SendItemType SendItem1)
		{
			object[] array = base.Invoke("SendItem", new object[]
			{
				SendItem1
			});
			return (SendItemResponseType)array[0];
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001222C File Offset: 0x0001042C
		public IAsyncResult BeginSendItem(SendItemType SendItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SendItem", new object[]
			{
				SendItem1
			}, callback, asyncState);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00012254 File Offset: 0x00010454
		public SendItemResponseType EndSendItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SendItemResponseType)array[0];
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012271 File Offset: 0x00010471
		public void SendItemAsync(SendItemType SendItem1)
		{
			this.SendItemAsync(SendItem1, null);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001227C File Offset: 0x0001047C
		public void SendItemAsync(SendItemType SendItem1, object userState)
		{
			if (this.SendItemOperationCompleted == null)
			{
				this.SendItemOperationCompleted = new SendOrPostCallback(this.OnSendItemOperationCompleted);
			}
			base.InvokeAsync("SendItem", new object[]
			{
				SendItem1
			}, this.SendItemOperationCompleted, userState);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000122C4 File Offset: 0x000104C4
		private void OnSendItemOperationCompleted(object arg)
		{
			if (this.SendItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SendItemCompleted(this, new SendItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001230C File Offset: 0x0001050C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MoveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("MoveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MoveItemResponseType MoveItem([XmlElement("MoveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MoveItemType MoveItem1)
		{
			object[] array = base.Invoke("MoveItem", new object[]
			{
				MoveItem1
			});
			return (MoveItemResponseType)array[0];
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001233C File Offset: 0x0001053C
		public IAsyncResult BeginMoveItem(MoveItemType MoveItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MoveItem", new object[]
			{
				MoveItem1
			}, callback, asyncState);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012364 File Offset: 0x00010564
		public MoveItemResponseType EndMoveItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MoveItemResponseType)array[0];
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012381 File Offset: 0x00010581
		public void MoveItemAsync(MoveItemType MoveItem1)
		{
			this.MoveItemAsync(MoveItem1, null);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001238C File Offset: 0x0001058C
		public void MoveItemAsync(MoveItemType MoveItem1, object userState)
		{
			if (this.MoveItemOperationCompleted == null)
			{
				this.MoveItemOperationCompleted = new SendOrPostCallback(this.OnMoveItemOperationCompleted);
			}
			base.InvokeAsync("MoveItem", new object[]
			{
				MoveItem1
			}, this.MoveItemOperationCompleted, userState);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000123D4 File Offset: 0x000105D4
		private void OnMoveItemOperationCompleted(object arg)
		{
			if (this.MoveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MoveItemCompleted(this, new MoveItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001241C File Offset: 0x0001061C
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CopyItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("CopyItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CopyItemResponseType CopyItem([XmlElement("CopyItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CopyItemType CopyItem1)
		{
			object[] array = base.Invoke("CopyItem", new object[]
			{
				CopyItem1
			});
			return (CopyItemResponseType)array[0];
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001244C File Offset: 0x0001064C
		public IAsyncResult BeginCopyItem(CopyItemType CopyItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CopyItem", new object[]
			{
				CopyItem1
			}, callback, asyncState);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012474 File Offset: 0x00010674
		public CopyItemResponseType EndCopyItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CopyItemResponseType)array[0];
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012491 File Offset: 0x00010691
		public void CopyItemAsync(CopyItemType CopyItem1)
		{
			this.CopyItemAsync(CopyItem1, null);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001249C File Offset: 0x0001069C
		public void CopyItemAsync(CopyItemType CopyItem1, object userState)
		{
			if (this.CopyItemOperationCompleted == null)
			{
				this.CopyItemOperationCompleted = new SendOrPostCallback(this.OnCopyItemOperationCompleted);
			}
			base.InvokeAsync("CopyItem", new object[]
			{
				CopyItem1
			}, this.CopyItemOperationCompleted, userState);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000124E4 File Offset: 0x000106E4
		private void OnCopyItemOperationCompleted(object arg)
		{
			if (this.CopyItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CopyItemCompleted(this, new CopyItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001252C File Offset: 0x0001072C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ArchiveItem", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("ArchiveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ArchiveItemResponseType ArchiveItem([XmlElement("ArchiveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ArchiveItemType ArchiveItem1)
		{
			object[] array = base.Invoke("ArchiveItem", new object[]
			{
				ArchiveItem1
			});
			return (ArchiveItemResponseType)array[0];
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001255C File Offset: 0x0001075C
		public IAsyncResult BeginArchiveItem(ArchiveItemType ArchiveItem1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ArchiveItem", new object[]
			{
				ArchiveItem1
			}, callback, asyncState);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012584 File Offset: 0x00010784
		public ArchiveItemResponseType EndArchiveItem(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ArchiveItemResponseType)array[0];
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000125A1 File Offset: 0x000107A1
		public void ArchiveItemAsync(ArchiveItemType ArchiveItem1)
		{
			this.ArchiveItemAsync(ArchiveItem1, null);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000125AC File Offset: 0x000107AC
		public void ArchiveItemAsync(ArchiveItemType ArchiveItem1, object userState)
		{
			if (this.ArchiveItemOperationCompleted == null)
			{
				this.ArchiveItemOperationCompleted = new SendOrPostCallback(this.OnArchiveItemOperationCompleted);
			}
			base.InvokeAsync("ArchiveItem", new object[]
			{
				ArchiveItem1
			}, this.ArchiveItemOperationCompleted, userState);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000125F4 File Offset: 0x000107F4
		private void OnArchiveItemOperationCompleted(object arg)
		{
			if (this.ArchiveItemCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ArchiveItemCompleted(this, new ArchiveItemCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001263C File Offset: 0x0001083C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("TimeZoneContext")]
		[return: XmlElement("CreateAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateAttachmentResponseType CreateAttachment([XmlElement("CreateAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateAttachmentType CreateAttachment1)
		{
			object[] array = base.Invoke("CreateAttachment", new object[]
			{
				CreateAttachment1
			});
			return (CreateAttachmentResponseType)array[0];
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001266C File Offset: 0x0001086C
		public IAsyncResult BeginCreateAttachment(CreateAttachmentType CreateAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateAttachment", new object[]
			{
				CreateAttachment1
			}, callback, asyncState);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00012694 File Offset: 0x00010894
		public CreateAttachmentResponseType EndCreateAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateAttachmentResponseType)array[0];
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000126B1 File Offset: 0x000108B1
		public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1)
		{
			this.CreateAttachmentAsync(CreateAttachment1, null);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000126BC File Offset: 0x000108BC
		public void CreateAttachmentAsync(CreateAttachmentType CreateAttachment1, object userState)
		{
			if (this.CreateAttachmentOperationCompleted == null)
			{
				this.CreateAttachmentOperationCompleted = new SendOrPostCallback(this.OnCreateAttachmentOperationCompleted);
			}
			base.InvokeAsync("CreateAttachment", new object[]
			{
				CreateAttachment1
			}, this.CreateAttachmentOperationCompleted, userState);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00012704 File Offset: 0x00010904
		private void OnCreateAttachmentOperationCompleted(object arg)
		{
			if (this.CreateAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateAttachmentCompleted(this, new CreateAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001274C File Offset: 0x0001094C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("DeleteAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteAttachmentResponseType DeleteAttachment([XmlElement("DeleteAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteAttachmentType DeleteAttachment1)
		{
			object[] array = base.Invoke("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			});
			return (DeleteAttachmentResponseType)array[0];
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001277C File Offset: 0x0001097C
		public IAsyncResult BeginDeleteAttachment(DeleteAttachmentType DeleteAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			}, callback, asyncState);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000127A4 File Offset: 0x000109A4
		public DeleteAttachmentResponseType EndDeleteAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteAttachmentResponseType)array[0];
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000127C1 File Offset: 0x000109C1
		public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1)
		{
			this.DeleteAttachmentAsync(DeleteAttachment1, null);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000127CC File Offset: 0x000109CC
		public void DeleteAttachmentAsync(DeleteAttachmentType DeleteAttachment1, object userState)
		{
			if (this.DeleteAttachmentOperationCompleted == null)
			{
				this.DeleteAttachmentOperationCompleted = new SendOrPostCallback(this.OnDeleteAttachmentOperationCompleted);
			}
			base.InvokeAsync("DeleteAttachment", new object[]
			{
				DeleteAttachment1
			}, this.DeleteAttachmentOperationCompleted, userState);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00012814 File Offset: 0x00010A14
		private void OnDeleteAttachmentOperationCompleted(object arg)
		{
			if (this.DeleteAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteAttachmentCompleted(this, new DeleteAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001285C File Offset: 0x00010A5C
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAttachment", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("GetAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAttachmentResponseType GetAttachment([XmlElement("GetAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAttachmentType GetAttachment1)
		{
			object[] array = base.Invoke("GetAttachment", new object[]
			{
				GetAttachment1
			});
			return (GetAttachmentResponseType)array[0];
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001288C File Offset: 0x00010A8C
		public IAsyncResult BeginGetAttachment(GetAttachmentType GetAttachment1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAttachment", new object[]
			{
				GetAttachment1
			}, callback, asyncState);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000128B4 File Offset: 0x00010AB4
		public GetAttachmentResponseType EndGetAttachment(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAttachmentResponseType)array[0];
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000128D1 File Offset: 0x00010AD1
		public void GetAttachmentAsync(GetAttachmentType GetAttachment1)
		{
			this.GetAttachmentAsync(GetAttachment1, null);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000128DC File Offset: 0x00010ADC
		public void GetAttachmentAsync(GetAttachmentType GetAttachment1, object userState)
		{
			if (this.GetAttachmentOperationCompleted == null)
			{
				this.GetAttachmentOperationCompleted = new SendOrPostCallback(this.OnGetAttachmentOperationCompleted);
			}
			base.InvokeAsync("GetAttachment", new object[]
			{
				GetAttachment1
			}, this.GetAttachmentOperationCompleted, userState);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00012924 File Offset: 0x00010B24
		private void OnGetAttachmentOperationCompleted(object arg)
		{
			if (this.GetAttachmentCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAttachmentCompleted(this, new GetAttachmentCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001296C File Offset: 0x00010B6C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetClientAccessToken", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetClientAccessTokenResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetClientAccessTokenResponseType GetClientAccessToken([XmlElement("GetClientAccessToken", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetClientAccessTokenType GetClientAccessToken1)
		{
			object[] array = base.Invoke("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			});
			return (GetClientAccessTokenResponseType)array[0];
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001299C File Offset: 0x00010B9C
		public IAsyncResult BeginGetClientAccessToken(GetClientAccessTokenType GetClientAccessToken1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			}, callback, asyncState);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000129C4 File Offset: 0x00010BC4
		public GetClientAccessTokenResponseType EndGetClientAccessToken(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetClientAccessTokenResponseType)array[0];
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000129E1 File Offset: 0x00010BE1
		public void GetClientAccessTokenAsync(GetClientAccessTokenType GetClientAccessToken1)
		{
			this.GetClientAccessTokenAsync(GetClientAccessToken1, null);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000129EC File Offset: 0x00010BEC
		public void GetClientAccessTokenAsync(GetClientAccessTokenType GetClientAccessToken1, object userState)
		{
			if (this.GetClientAccessTokenOperationCompleted == null)
			{
				this.GetClientAccessTokenOperationCompleted = new SendOrPostCallback(this.OnGetClientAccessTokenOperationCompleted);
			}
			base.InvokeAsync("GetClientAccessToken", new object[]
			{
				GetClientAccessToken1
			}, this.GetClientAccessTokenOperationCompleted, userState);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012A34 File Offset: 0x00010C34
		private void OnGetClientAccessTokenOperationCompleted(object arg)
		{
			if (this.GetClientAccessTokenCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetClientAccessTokenCompleted(this, new GetClientAccessTokenCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00012A7C File Offset: 0x00010C7C
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetDelegateResponseMessageType GetDelegate([XmlElement("GetDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDelegateType GetDelegate1)
		{
			object[] array = base.Invoke("GetDelegate", new object[]
			{
				GetDelegate1
			});
			return (GetDelegateResponseMessageType)array[0];
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00012AAC File Offset: 0x00010CAC
		public IAsyncResult BeginGetDelegate(GetDelegateType GetDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDelegate", new object[]
			{
				GetDelegate1
			}, callback, asyncState);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00012AD4 File Offset: 0x00010CD4
		public GetDelegateResponseMessageType EndGetDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00012AF1 File Offset: 0x00010CF1
		public void GetDelegateAsync(GetDelegateType GetDelegate1)
		{
			this.GetDelegateAsync(GetDelegate1, null);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012AFC File Offset: 0x00010CFC
		public void GetDelegateAsync(GetDelegateType GetDelegate1, object userState)
		{
			if (this.GetDelegateOperationCompleted == null)
			{
				this.GetDelegateOperationCompleted = new SendOrPostCallback(this.OnGetDelegateOperationCompleted);
			}
			base.InvokeAsync("GetDelegate", new object[]
			{
				GetDelegate1
			}, this.GetDelegateOperationCompleted, userState);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012B44 File Offset: 0x00010D44
		private void OnGetDelegateOperationCompleted(object arg)
		{
			if (this.GetDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDelegateCompleted(this, new GetDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012B8C File Offset: 0x00010D8C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("AddDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddDelegateResponseMessageType AddDelegate([XmlElement("AddDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDelegateType AddDelegate1)
		{
			object[] array = base.Invoke("AddDelegate", new object[]
			{
				AddDelegate1
			});
			return (AddDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012BBC File Offset: 0x00010DBC
		public IAsyncResult BeginAddDelegate(AddDelegateType AddDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddDelegate", new object[]
			{
				AddDelegate1
			}, callback, asyncState);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public AddDelegateResponseMessageType EndAddDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012C01 File Offset: 0x00010E01
		public void AddDelegateAsync(AddDelegateType AddDelegate1)
		{
			this.AddDelegateAsync(AddDelegate1, null);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00012C0C File Offset: 0x00010E0C
		public void AddDelegateAsync(AddDelegateType AddDelegate1, object userState)
		{
			if (this.AddDelegateOperationCompleted == null)
			{
				this.AddDelegateOperationCompleted = new SendOrPostCallback(this.OnAddDelegateOperationCompleted);
			}
			base.InvokeAsync("AddDelegate", new object[]
			{
				AddDelegate1
			}, this.AddDelegateOperationCompleted, userState);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00012C54 File Offset: 0x00010E54
		private void OnAddDelegateOperationCompleted(object arg)
		{
			if (this.AddDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddDelegateCompleted(this, new AddDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012C9C File Offset: 0x00010E9C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("RemoveDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveDelegateResponseMessageType RemoveDelegate([XmlElement("RemoveDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDelegateType RemoveDelegate1)
		{
			object[] array = base.Invoke("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			});
			return (RemoveDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00012CCC File Offset: 0x00010ECC
		public IAsyncResult BeginRemoveDelegate(RemoveDelegateType RemoveDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			}, callback, asyncState);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public RemoveDelegateResponseMessageType EndRemoveDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012D11 File Offset: 0x00010F11
		public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1)
		{
			this.RemoveDelegateAsync(RemoveDelegate1, null);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00012D1C File Offset: 0x00010F1C
		public void RemoveDelegateAsync(RemoveDelegateType RemoveDelegate1, object userState)
		{
			if (this.RemoveDelegateOperationCompleted == null)
			{
				this.RemoveDelegateOperationCompleted = new SendOrPostCallback(this.OnRemoveDelegateOperationCompleted);
			}
			base.InvokeAsync("RemoveDelegate", new object[]
			{
				RemoveDelegate1
			}, this.RemoveDelegateOperationCompleted, userState);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012D64 File Offset: 0x00010F64
		private void OnRemoveDelegateOperationCompleted(object arg)
		{
			if (this.RemoveDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveDelegateCompleted(this, new RemoveDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00012DAC File Offset: 0x00010FAC
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateDelegate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UpdateDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateDelegateResponseMessageType UpdateDelegate([XmlElement("UpdateDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateDelegateType UpdateDelegate1)
		{
			object[] array = base.Invoke("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			});
			return (UpdateDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012DDC File Offset: 0x00010FDC
		public IAsyncResult BeginUpdateDelegate(UpdateDelegateType UpdateDelegate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			}, callback, asyncState);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012E04 File Offset: 0x00011004
		public UpdateDelegateResponseMessageType EndUpdateDelegate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateDelegateResponseMessageType)array[0];
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012E21 File Offset: 0x00011021
		public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1)
		{
			this.UpdateDelegateAsync(UpdateDelegate1, null);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00012E2C File Offset: 0x0001102C
		public void UpdateDelegateAsync(UpdateDelegateType UpdateDelegate1, object userState)
		{
			if (this.UpdateDelegateOperationCompleted == null)
			{
				this.UpdateDelegateOperationCompleted = new SendOrPostCallback(this.OnUpdateDelegateOperationCompleted);
			}
			base.InvokeAsync("UpdateDelegate", new object[]
			{
				UpdateDelegate1
			}, this.UpdateDelegateOperationCompleted, userState);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012E74 File Offset: 0x00011074
		private void OnUpdateDelegateOperationCompleted(object arg)
		{
			if (this.UpdateDelegateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateDelegateCompleted(this, new UpdateDelegateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012EBC File Offset: 0x000110BC
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/CreateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("CreateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public CreateUserConfigurationResponseType CreateUserConfiguration([XmlElement("CreateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] CreateUserConfigurationType CreateUserConfiguration1)
		{
			object[] array = base.Invoke("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			});
			return (CreateUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012EEC File Offset: 0x000110EC
		public IAsyncResult BeginCreateUserConfiguration(CreateUserConfigurationType CreateUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012F14 File Offset: 0x00011114
		public CreateUserConfigurationResponseType EndCreateUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CreateUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00012F31 File Offset: 0x00011131
		public void CreateUserConfigurationAsync(CreateUserConfigurationType CreateUserConfiguration1)
		{
			this.CreateUserConfigurationAsync(CreateUserConfiguration1, null);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012F3C File Offset: 0x0001113C
		public void CreateUserConfigurationAsync(CreateUserConfigurationType CreateUserConfiguration1, object userState)
		{
			if (this.CreateUserConfigurationOperationCompleted == null)
			{
				this.CreateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnCreateUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("CreateUserConfiguration", new object[]
			{
				CreateUserConfiguration1
			}, this.CreateUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012F84 File Offset: 0x00011184
		private void OnCreateUserConfigurationOperationCompleted(object arg)
		{
			if (this.CreateUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateUserConfigurationCompleted(this, new CreateUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00012FCC File Offset: 0x000111CC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DeleteUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("DeleteUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DeleteUserConfigurationResponseType DeleteUserConfiguration([XmlElement("DeleteUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DeleteUserConfigurationType DeleteUserConfiguration1)
		{
			object[] array = base.Invoke("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			});
			return (DeleteUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012FFC File Offset: 0x000111FC
		public IAsyncResult BeginDeleteUserConfiguration(DeleteUserConfigurationType DeleteUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013024 File Offset: 0x00011224
		public DeleteUserConfigurationResponseType EndDeleteUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DeleteUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00013041 File Offset: 0x00011241
		public void DeleteUserConfigurationAsync(DeleteUserConfigurationType DeleteUserConfiguration1)
		{
			this.DeleteUserConfigurationAsync(DeleteUserConfiguration1, null);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001304C File Offset: 0x0001124C
		public void DeleteUserConfigurationAsync(DeleteUserConfigurationType DeleteUserConfiguration1, object userState)
		{
			if (this.DeleteUserConfigurationOperationCompleted == null)
			{
				this.DeleteUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnDeleteUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("DeleteUserConfiguration", new object[]
			{
				DeleteUserConfiguration1
			}, this.DeleteUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013094 File Offset: 0x00011294
		private void OnDeleteUserConfigurationOperationCompleted(object arg)
		{
			if (this.DeleteUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteUserConfigurationCompleted(this, new DeleteUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000130DC File Offset: 0x000112DC
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserConfigurationResponseType GetUserConfiguration([XmlElement("GetUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserConfigurationType GetUserConfiguration1)
		{
			object[] array = base.Invoke("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			});
			return (GetUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001310C File Offset: 0x0001130C
		public IAsyncResult BeginGetUserConfiguration(GetUserConfigurationType GetUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013134 File Offset: 0x00011334
		public GetUserConfigurationResponseType EndGetUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013151 File Offset: 0x00011351
		public void GetUserConfigurationAsync(GetUserConfigurationType GetUserConfiguration1)
		{
			this.GetUserConfigurationAsync(GetUserConfiguration1, null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001315C File Offset: 0x0001135C
		public void GetUserConfigurationAsync(GetUserConfigurationType GetUserConfiguration1, object userState)
		{
			if (this.GetUserConfigurationOperationCompleted == null)
			{
				this.GetUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetUserConfiguration", new object[]
			{
				GetUserConfiguration1
			}, this.GetUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000131A4 File Offset: 0x000113A4
		private void OnGetUserConfigurationOperationCompleted(object arg)
		{
			if (this.GetUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserConfigurationCompleted(this, new GetUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000131EC File Offset: 0x000113EC
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateUserConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("UpdateUserConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateUserConfigurationResponseType UpdateUserConfiguration([XmlElement("UpdateUserConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateUserConfigurationType UpdateUserConfiguration1)
		{
			object[] array = base.Invoke("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			});
			return (UpdateUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001321C File Offset: 0x0001141C
		public IAsyncResult BeginUpdateUserConfiguration(UpdateUserConfigurationType UpdateUserConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013244 File Offset: 0x00011444
		public UpdateUserConfigurationResponseType EndUpdateUserConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateUserConfigurationResponseType)array[0];
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013261 File Offset: 0x00011461
		public void UpdateUserConfigurationAsync(UpdateUserConfigurationType UpdateUserConfiguration1)
		{
			this.UpdateUserConfigurationAsync(UpdateUserConfiguration1, null);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001326C File Offset: 0x0001146C
		public void UpdateUserConfigurationAsync(UpdateUserConfigurationType UpdateUserConfiguration1, object userState)
		{
			if (this.UpdateUserConfigurationOperationCompleted == null)
			{
				this.UpdateUserConfigurationOperationCompleted = new SendOrPostCallback(this.OnUpdateUserConfigurationOperationCompleted);
			}
			base.InvokeAsync("UpdateUserConfiguration", new object[]
			{
				UpdateUserConfiguration1
			}, this.UpdateUserConfigurationOperationCompleted, userState);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000132B4 File Offset: 0x000114B4
		private void OnUpdateUserConfigurationOperationCompleted(object arg)
		{
			if (this.UpdateUserConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateUserConfigurationCompleted(this, new UpdateUserConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000132FC File Offset: 0x000114FC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserAvailability", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("GetUserAvailabilityResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserAvailabilityResponseType GetUserAvailability([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserAvailabilityRequestType GetUserAvailabilityRequest)
		{
			object[] array = base.Invoke("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			});
			return (GetUserAvailabilityResponseType)array[0];
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001332C File Offset: 0x0001152C
		public IAsyncResult BeginGetUserAvailability(GetUserAvailabilityRequestType GetUserAvailabilityRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			}, callback, asyncState);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013354 File Offset: 0x00011554
		public GetUserAvailabilityResponseType EndGetUserAvailability(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserAvailabilityResponseType)array[0];
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013371 File Offset: 0x00011571
		public void GetUserAvailabilityAsync(GetUserAvailabilityRequestType GetUserAvailabilityRequest)
		{
			this.GetUserAvailabilityAsync(GetUserAvailabilityRequest, null);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001337C File Offset: 0x0001157C
		public void GetUserAvailabilityAsync(GetUserAvailabilityRequestType GetUserAvailabilityRequest, object userState)
		{
			if (this.GetUserAvailabilityOperationCompleted == null)
			{
				this.GetUserAvailabilityOperationCompleted = new SendOrPostCallback(this.OnGetUserAvailabilityOperationCompleted);
			}
			base.InvokeAsync("GetUserAvailability", new object[]
			{
				GetUserAvailabilityRequest
			}, this.GetUserAvailabilityOperationCompleted, userState);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000133C4 File Offset: 0x000115C4
		private void OnGetUserAvailabilityOperationCompleted(object arg)
		{
			if (this.GetUserAvailabilityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserAvailabilityCompleted(this, new GetUserAvailabilityCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001340C File Offset: 0x0001160C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserOofSettingsResponse GetUserOofSettings([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserOofSettingsRequest GetUserOofSettingsRequest)
		{
			object[] array = base.Invoke("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			});
			return (GetUserOofSettingsResponse)array[0];
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001343C File Offset: 0x0001163C
		public IAsyncResult BeginGetUserOofSettings(GetUserOofSettingsRequest GetUserOofSettingsRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			}, callback, asyncState);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013464 File Offset: 0x00011664
		public GetUserOofSettingsResponse EndGetUserOofSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserOofSettingsResponse)array[0];
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00013481 File Offset: 0x00011681
		public void GetUserOofSettingsAsync(GetUserOofSettingsRequest GetUserOofSettingsRequest)
		{
			this.GetUserOofSettingsAsync(GetUserOofSettingsRequest, null);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001348C File Offset: 0x0001168C
		public void GetUserOofSettingsAsync(GetUserOofSettingsRequest GetUserOofSettingsRequest, object userState)
		{
			if (this.GetUserOofSettingsOperationCompleted == null)
			{
				this.GetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnGetUserOofSettingsOperationCompleted);
			}
			base.InvokeAsync("GetUserOofSettings", new object[]
			{
				GetUserOofSettingsRequest
			}, this.GetUserOofSettingsOperationCompleted, userState);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000134D4 File Offset: 0x000116D4
		private void OnGetUserOofSettingsOperationCompleted(object arg)
		{
			if (this.GetUserOofSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserOofSettingsCompleted(this, new GetUserOofSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001351C File Offset: 0x0001171C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetUserOofSettings", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("SetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetUserOofSettingsResponse SetUserOofSettings([XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetUserOofSettingsRequest SetUserOofSettingsRequest)
		{
			object[] array = base.Invoke("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			});
			return (SetUserOofSettingsResponse)array[0];
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001354C File Offset: 0x0001174C
		public IAsyncResult BeginSetUserOofSettings(SetUserOofSettingsRequest SetUserOofSettingsRequest, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			}, callback, asyncState);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00013574 File Offset: 0x00011774
		public SetUserOofSettingsResponse EndSetUserOofSettings(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetUserOofSettingsResponse)array[0];
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00013591 File Offset: 0x00011791
		public void SetUserOofSettingsAsync(SetUserOofSettingsRequest SetUserOofSettingsRequest)
		{
			this.SetUserOofSettingsAsync(SetUserOofSettingsRequest, null);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001359C File Offset: 0x0001179C
		public void SetUserOofSettingsAsync(SetUserOofSettingsRequest SetUserOofSettingsRequest, object userState)
		{
			if (this.SetUserOofSettingsOperationCompleted == null)
			{
				this.SetUserOofSettingsOperationCompleted = new SendOrPostCallback(this.OnSetUserOofSettingsOperationCompleted);
			}
			base.InvokeAsync("SetUserOofSettings", new object[]
			{
				SetUserOofSettingsRequest
			}, this.SetUserOofSettingsOperationCompleted, userState);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000135E4 File Offset: 0x000117E4
		private void OnSetUserOofSettingsOperationCompleted(object arg)
		{
			if (this.SetUserOofSettingsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetUserOofSettingsCompleted(this, new SetUserOofSettingsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001362C File Offset: 0x0001182C
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetServiceConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetServiceConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetServiceConfigurationResponseMessageType GetServiceConfiguration([XmlElement("GetServiceConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetServiceConfigurationType GetServiceConfiguration1)
		{
			object[] array = base.Invoke("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			});
			return (GetServiceConfigurationResponseMessageType)array[0];
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001365C File Offset: 0x0001185C
		public IAsyncResult BeginGetServiceConfiguration(GetServiceConfigurationType GetServiceConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00013684 File Offset: 0x00011884
		public GetServiceConfigurationResponseMessageType EndGetServiceConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetServiceConfigurationResponseMessageType)array[0];
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000136A1 File Offset: 0x000118A1
		public void GetServiceConfigurationAsync(GetServiceConfigurationType GetServiceConfiguration1)
		{
			this.GetServiceConfigurationAsync(GetServiceConfiguration1, null);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000136AC File Offset: 0x000118AC
		public void GetServiceConfigurationAsync(GetServiceConfigurationType GetServiceConfiguration1, object userState)
		{
			if (this.GetServiceConfigurationOperationCompleted == null)
			{
				this.GetServiceConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetServiceConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetServiceConfiguration", new object[]
			{
				GetServiceConfiguration1
			}, this.GetServiceConfigurationOperationCompleted, userState);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000136F4 File Offset: 0x000118F4
		private void OnGetServiceConfigurationOperationCompleted(object arg)
		{
			if (this.GetServiceConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceConfigurationCompleted(this, new GetServiceConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001373C File Offset: 0x0001193C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMailTips", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetMailTipsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMailTipsResponseMessageType GetMailTips([XmlElement("GetMailTips", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMailTipsType GetMailTips1)
		{
			object[] array = base.Invoke("GetMailTips", new object[]
			{
				GetMailTips1
			});
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001376C File Offset: 0x0001196C
		public IAsyncResult BeginGetMailTips(GetMailTipsType GetMailTips1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMailTips", new object[]
			{
				GetMailTips1
			}, callback, asyncState);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00013794 File Offset: 0x00011994
		public GetMailTipsResponseMessageType EndGetMailTips(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMailTipsResponseMessageType)array[0];
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000137B1 File Offset: 0x000119B1
		public void GetMailTipsAsync(GetMailTipsType GetMailTips1)
		{
			this.GetMailTipsAsync(GetMailTips1, null);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000137BC File Offset: 0x000119BC
		public void GetMailTipsAsync(GetMailTipsType GetMailTips1, object userState)
		{
			if (this.GetMailTipsOperationCompleted == null)
			{
				this.GetMailTipsOperationCompleted = new SendOrPostCallback(this.OnGetMailTipsOperationCompleted);
			}
			base.InvokeAsync("GetMailTips", new object[]
			{
				GetMailTips1
			}, this.GetMailTipsOperationCompleted, userState);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00013804 File Offset: 0x00011A04
		private void OnGetMailTipsOperationCompleted(object arg)
		{
			if (this.GetMailTipsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMailTipsCompleted(this, new GetMailTipsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001384C File Offset: 0x00011A4C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/PlayOnPhone", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("PlayOnPhoneResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public PlayOnPhoneResponseMessageType PlayOnPhone([XmlElement("PlayOnPhone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] PlayOnPhoneType PlayOnPhone1)
		{
			object[] array = base.Invoke("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			});
			return (PlayOnPhoneResponseMessageType)array[0];
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001387C File Offset: 0x00011A7C
		public IAsyncResult BeginPlayOnPhone(PlayOnPhoneType PlayOnPhone1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			}, callback, asyncState);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000138A4 File Offset: 0x00011AA4
		public PlayOnPhoneResponseMessageType EndPlayOnPhone(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (PlayOnPhoneResponseMessageType)array[0];
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000138C1 File Offset: 0x00011AC1
		public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1)
		{
			this.PlayOnPhoneAsync(PlayOnPhone1, null);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000138CC File Offset: 0x00011ACC
		public void PlayOnPhoneAsync(PlayOnPhoneType PlayOnPhone1, object userState)
		{
			if (this.PlayOnPhoneOperationCompleted == null)
			{
				this.PlayOnPhoneOperationCompleted = new SendOrPostCallback(this.OnPlayOnPhoneOperationCompleted);
			}
			base.InvokeAsync("PlayOnPhone", new object[]
			{
				PlayOnPhone1
			}, this.PlayOnPhoneOperationCompleted, userState);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00013914 File Offset: 0x00011B14
		private void OnPlayOnPhoneOperationCompleted(object arg)
		{
			if (this.PlayOnPhoneCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PlayOnPhoneCompleted(this, new PlayOnPhoneCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001395C File Offset: 0x00011B5C
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPhoneCallInformation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetPhoneCallInformationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPhoneCallInformationResponseMessageType GetPhoneCallInformation([XmlElement("GetPhoneCallInformation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPhoneCallInformationType GetPhoneCallInformation1)
		{
			object[] array = base.Invoke("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			});
			return (GetPhoneCallInformationResponseMessageType)array[0];
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001398C File Offset: 0x00011B8C
		public IAsyncResult BeginGetPhoneCallInformation(GetPhoneCallInformationType GetPhoneCallInformation1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			}, callback, asyncState);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000139B4 File Offset: 0x00011BB4
		public GetPhoneCallInformationResponseMessageType EndGetPhoneCallInformation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPhoneCallInformationResponseMessageType)array[0];
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000139D1 File Offset: 0x00011BD1
		public void GetPhoneCallInformationAsync(GetPhoneCallInformationType GetPhoneCallInformation1)
		{
			this.GetPhoneCallInformationAsync(GetPhoneCallInformation1, null);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000139DC File Offset: 0x00011BDC
		public void GetPhoneCallInformationAsync(GetPhoneCallInformationType GetPhoneCallInformation1, object userState)
		{
			if (this.GetPhoneCallInformationOperationCompleted == null)
			{
				this.GetPhoneCallInformationOperationCompleted = new SendOrPostCallback(this.OnGetPhoneCallInformationOperationCompleted);
			}
			base.InvokeAsync("GetPhoneCallInformation", new object[]
			{
				GetPhoneCallInformation1
			}, this.GetPhoneCallInformationOperationCompleted, userState);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00013A24 File Offset: 0x00011C24
		private void OnGetPhoneCallInformationOperationCompleted(object arg)
		{
			if (this.GetPhoneCallInformationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPhoneCallInformationCompleted(this, new GetPhoneCallInformationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00013A6C File Offset: 0x00011C6C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisconnectPhoneCall", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("DisconnectPhoneCallResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DisconnectPhoneCallResponseMessageType DisconnectPhoneCall([XmlElement("DisconnectPhoneCall", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisconnectPhoneCallType DisconnectPhoneCall1)
		{
			object[] array = base.Invoke("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			});
			return (DisconnectPhoneCallResponseMessageType)array[0];
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00013A9C File Offset: 0x00011C9C
		public IAsyncResult BeginDisconnectPhoneCall(DisconnectPhoneCallType DisconnectPhoneCall1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			}, callback, asyncState);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00013AC4 File Offset: 0x00011CC4
		public DisconnectPhoneCallResponseMessageType EndDisconnectPhoneCall(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DisconnectPhoneCallResponseMessageType)array[0];
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00013AE1 File Offset: 0x00011CE1
		public void DisconnectPhoneCallAsync(DisconnectPhoneCallType DisconnectPhoneCall1)
		{
			this.DisconnectPhoneCallAsync(DisconnectPhoneCall1, null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00013AEC File Offset: 0x00011CEC
		public void DisconnectPhoneCallAsync(DisconnectPhoneCallType DisconnectPhoneCall1, object userState)
		{
			if (this.DisconnectPhoneCallOperationCompleted == null)
			{
				this.DisconnectPhoneCallOperationCompleted = new SendOrPostCallback(this.OnDisconnectPhoneCallOperationCompleted);
			}
			base.InvokeAsync("DisconnectPhoneCall", new object[]
			{
				DisconnectPhoneCall1
			}, this.DisconnectPhoneCallOperationCompleted, userState);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013B34 File Offset: 0x00011D34
		private void OnDisconnectPhoneCallOperationCompleted(object arg)
		{
			if (this.DisconnectPhoneCallCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisconnectPhoneCallCompleted(this, new DisconnectPhoneCallCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00013B7C File Offset: 0x00011D7C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingMetadata", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetSharingMetadataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSharingMetadataResponseMessageType GetSharingMetadata([XmlElement("GetSharingMetadata", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingMetadataType GetSharingMetadata1)
		{
			object[] array = base.Invoke("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			});
			return (GetSharingMetadataResponseMessageType)array[0];
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013BAC File Offset: 0x00011DAC
		public IAsyncResult BeginGetSharingMetadata(GetSharingMetadataType GetSharingMetadata1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			}, callback, asyncState);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public GetSharingMetadataResponseMessageType EndGetSharingMetadata(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSharingMetadataResponseMessageType)array[0];
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013BF1 File Offset: 0x00011DF1
		public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1)
		{
			this.GetSharingMetadataAsync(GetSharingMetadata1, null);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00013BFC File Offset: 0x00011DFC
		public void GetSharingMetadataAsync(GetSharingMetadataType GetSharingMetadata1, object userState)
		{
			if (this.GetSharingMetadataOperationCompleted == null)
			{
				this.GetSharingMetadataOperationCompleted = new SendOrPostCallback(this.OnGetSharingMetadataOperationCompleted);
			}
			base.InvokeAsync("GetSharingMetadata", new object[]
			{
				GetSharingMetadata1
			}, this.GetSharingMetadataOperationCompleted, userState);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00013C44 File Offset: 0x00011E44
		private void OnGetSharingMetadataOperationCompleted(object arg)
		{
			if (this.GetSharingMetadataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSharingMetadataCompleted(this, new GetSharingMetadataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013C8C File Offset: 0x00011E8C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RefreshSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("RefreshSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RefreshSharingFolderResponseMessageType RefreshSharingFolder([XmlElement("RefreshSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RefreshSharingFolderType RefreshSharingFolder1)
		{
			object[] array = base.Invoke("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			});
			return (RefreshSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013CBC File Offset: 0x00011EBC
		public IAsyncResult BeginRefreshSharingFolder(RefreshSharingFolderType RefreshSharingFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			}, callback, asyncState);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013CE4 File Offset: 0x00011EE4
		public RefreshSharingFolderResponseMessageType EndRefreshSharingFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RefreshSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013D01 File Offset: 0x00011F01
		public void RefreshSharingFolderAsync(RefreshSharingFolderType RefreshSharingFolder1)
		{
			this.RefreshSharingFolderAsync(RefreshSharingFolder1, null);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013D0C File Offset: 0x00011F0C
		public void RefreshSharingFolderAsync(RefreshSharingFolderType RefreshSharingFolder1, object userState)
		{
			if (this.RefreshSharingFolderOperationCompleted == null)
			{
				this.RefreshSharingFolderOperationCompleted = new SendOrPostCallback(this.OnRefreshSharingFolderOperationCompleted);
			}
			base.InvokeAsync("RefreshSharingFolder", new object[]
			{
				RefreshSharingFolder1
			}, this.RefreshSharingFolderOperationCompleted, userState);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013D54 File Offset: 0x00011F54
		private void OnRefreshSharingFolderOperationCompleted(object arg)
		{
			if (this.RefreshSharingFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RefreshSharingFolderCompleted(this, new RefreshSharingFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013D9C File Offset: 0x00011F9C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSharingFolder", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSharingFolderResponseMessageType GetSharingFolder([XmlElement("GetSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSharingFolderType GetSharingFolder1)
		{
			object[] array = base.Invoke("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			});
			return (GetSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013DCC File Offset: 0x00011FCC
		public IAsyncResult BeginGetSharingFolder(GetSharingFolderType GetSharingFolder1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			}, callback, asyncState);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public GetSharingFolderResponseMessageType EndGetSharingFolder(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSharingFolderResponseMessageType)array[0];
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013E11 File Offset: 0x00012011
		public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1)
		{
			this.GetSharingFolderAsync(GetSharingFolder1, null);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013E1C File Offset: 0x0001201C
		public void GetSharingFolderAsync(GetSharingFolderType GetSharingFolder1, object userState)
		{
			if (this.GetSharingFolderOperationCompleted == null)
			{
				this.GetSharingFolderOperationCompleted = new SendOrPostCallback(this.OnGetSharingFolderOperationCompleted);
			}
			base.InvokeAsync("GetSharingFolder", new object[]
			{
				GetSharingFolder1
			}, this.GetSharingFolderOperationCompleted, userState);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013E64 File Offset: 0x00012064
		private void OnGetSharingFolderOperationCompleted(object arg)
		{
			if (this.GetSharingFolderCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSharingFolderCompleted(this, new GetSharingFolderCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013EAC File Offset: 0x000120AC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ManagementRole")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("SetTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetTeamMailboxResponseMessageType SetTeamMailbox([XmlElement("SetTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetTeamMailboxRequestType SetTeamMailbox1)
		{
			object[] array = base.Invoke("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			});
			return (SetTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013EDC File Offset: 0x000120DC
		public IAsyncResult BeginSetTeamMailbox(SetTeamMailboxRequestType SetTeamMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013F04 File Offset: 0x00012104
		public SetTeamMailboxResponseMessageType EndSetTeamMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013F21 File Offset: 0x00012121
		public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1)
		{
			this.SetTeamMailboxAsync(SetTeamMailbox1, null);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013F2C File Offset: 0x0001212C
		public void SetTeamMailboxAsync(SetTeamMailboxRequestType SetTeamMailbox1, object userState)
		{
			if (this.SetTeamMailboxOperationCompleted == null)
			{
				this.SetTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnSetTeamMailboxOperationCompleted);
			}
			base.InvokeAsync("SetTeamMailbox", new object[]
			{
				SetTeamMailbox1
			}, this.SetTeamMailboxOperationCompleted, userState);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013F74 File Offset: 0x00012174
		private void OnSetTeamMailboxOperationCompleted(object arg)
		{
			if (this.SetTeamMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetTeamMailboxCompleted(this, new SetTeamMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013FBC File Offset: 0x000121BC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UnpinTeamMailbox", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("UnpinTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UnpinTeamMailboxResponseMessageType UnpinTeamMailbox([XmlElement("UnpinTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UnpinTeamMailboxRequestType UnpinTeamMailbox1)
		{
			object[] array = base.Invoke("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			});
			return (UnpinTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013FEC File Offset: 0x000121EC
		public IAsyncResult BeginUnpinTeamMailbox(UnpinTeamMailboxRequestType UnpinTeamMailbox1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			}, callback, asyncState);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00014014 File Offset: 0x00012214
		public UnpinTeamMailboxResponseMessageType EndUnpinTeamMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UnpinTeamMailboxResponseMessageType)array[0];
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00014031 File Offset: 0x00012231
		public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1)
		{
			this.UnpinTeamMailboxAsync(UnpinTeamMailbox1, null);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001403C File Offset: 0x0001223C
		public void UnpinTeamMailboxAsync(UnpinTeamMailboxRequestType UnpinTeamMailbox1, object userState)
		{
			if (this.UnpinTeamMailboxOperationCompleted == null)
			{
				this.UnpinTeamMailboxOperationCompleted = new SendOrPostCallback(this.OnUnpinTeamMailboxOperationCompleted);
			}
			base.InvokeAsync("UnpinTeamMailbox", new object[]
			{
				UnpinTeamMailbox1
			}, this.UnpinTeamMailboxOperationCompleted, userState);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00014084 File Offset: 0x00012284
		private void OnUnpinTeamMailboxOperationCompleted(object arg)
		{
			if (this.UnpinTeamMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UnpinTeamMailboxCompleted(this, new UnpinTeamMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000140CC File Offset: 0x000122CC
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRoomLists", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetRoomListsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetRoomListsResponseMessageType GetRoomLists([XmlElement("GetRoomLists", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomListsType GetRoomLists1)
		{
			object[] array = base.Invoke("GetRoomLists", new object[]
			{
				GetRoomLists1
			});
			return (GetRoomListsResponseMessageType)array[0];
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000140FC File Offset: 0x000122FC
		public IAsyncResult BeginGetRoomLists(GetRoomListsType GetRoomLists1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRoomLists", new object[]
			{
				GetRoomLists1
			}, callback, asyncState);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00014124 File Offset: 0x00012324
		public GetRoomListsResponseMessageType EndGetRoomLists(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetRoomListsResponseMessageType)array[0];
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00014141 File Offset: 0x00012341
		public void GetRoomListsAsync(GetRoomListsType GetRoomLists1)
		{
			this.GetRoomListsAsync(GetRoomLists1, null);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001414C File Offset: 0x0001234C
		public void GetRoomListsAsync(GetRoomListsType GetRoomLists1, object userState)
		{
			if (this.GetRoomListsOperationCompleted == null)
			{
				this.GetRoomListsOperationCompleted = new SendOrPostCallback(this.OnGetRoomListsOperationCompleted);
			}
			base.InvokeAsync("GetRoomLists", new object[]
			{
				GetRoomLists1
			}, this.GetRoomListsOperationCompleted, userState);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00014194 File Offset: 0x00012394
		private void OnGetRoomListsOperationCompleted(object arg)
		{
			if (this.GetRoomListsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRoomListsCompleted(this, new GetRoomListsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000141DC File Offset: 0x000123DC
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetRooms", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetRoomsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetRoomsResponseMessageType GetRooms([XmlElement("GetRooms", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetRoomsType GetRooms1)
		{
			object[] array = base.Invoke("GetRooms", new object[]
			{
				GetRooms1
			});
			return (GetRoomsResponseMessageType)array[0];
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001420C File Offset: 0x0001240C
		public IAsyncResult BeginGetRooms(GetRoomsType GetRooms1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRooms", new object[]
			{
				GetRooms1
			}, callback, asyncState);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00014234 File Offset: 0x00012434
		public GetRoomsResponseMessageType EndGetRooms(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetRoomsResponseMessageType)array[0];
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00014251 File Offset: 0x00012451
		public void GetRoomsAsync(GetRoomsType GetRooms1)
		{
			this.GetRoomsAsync(GetRooms1, null);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001425C File Offset: 0x0001245C
		public void GetRoomsAsync(GetRoomsType GetRooms1, object userState)
		{
			if (this.GetRoomsOperationCompleted == null)
			{
				this.GetRoomsOperationCompleted = new SendOrPostCallback(this.OnGetRoomsOperationCompleted);
			}
			base.InvokeAsync("GetRooms", new object[]
			{
				GetRooms1
			}, this.GetRoomsOperationCompleted, userState);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000142A4 File Offset: 0x000124A4
		private void OnGetRoomsOperationCompleted(object arg)
		{
			if (this.GetRoomsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRoomsCompleted(this, new GetRoomsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000142EC File Offset: 0x000124EC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("FindMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindMessageTrackingReportResponseMessageType FindMessageTrackingReport([XmlElement("FindMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindMessageTrackingReportRequestType FindMessageTrackingReport1)
		{
			object[] array = base.Invoke("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			});
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001431C File Offset: 0x0001251C
		public IAsyncResult BeginFindMessageTrackingReport(FindMessageTrackingReportRequestType FindMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00014344 File Offset: 0x00012544
		public FindMessageTrackingReportResponseMessageType EndFindMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00014361 File Offset: 0x00012561
		public void FindMessageTrackingReportAsync(FindMessageTrackingReportRequestType FindMessageTrackingReport1)
		{
			this.FindMessageTrackingReportAsync(FindMessageTrackingReport1, null);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001436C File Offset: 0x0001256C
		public void FindMessageTrackingReportAsync(FindMessageTrackingReportRequestType FindMessageTrackingReport1, object userState)
		{
			if (this.FindMessageTrackingReportOperationCompleted == null)
			{
				this.FindMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnFindMessageTrackingReportOperationCompleted);
			}
			base.InvokeAsync("FindMessageTrackingReport", new object[]
			{
				FindMessageTrackingReport1
			}, this.FindMessageTrackingReportOperationCompleted, userState);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000143B4 File Offset: 0x000125B4
		private void OnFindMessageTrackingReportOperationCompleted(object arg)
		{
			if (this.FindMessageTrackingReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindMessageTrackingReportCompleted(this, new FindMessageTrackingReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000143FC File Offset: 0x000125FC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetMessageTrackingReport", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetMessageTrackingReportResponseMessageType GetMessageTrackingReport([XmlElement("GetMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetMessageTrackingReportRequestType GetMessageTrackingReport1)
		{
			object[] array = base.Invoke("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			});
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001442C File Offset: 0x0001262C
		public IAsyncResult BeginGetMessageTrackingReport(GetMessageTrackingReportRequestType GetMessageTrackingReport1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			}, callback, asyncState);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00014454 File Offset: 0x00012654
		public GetMessageTrackingReportResponseMessageType EndGetMessageTrackingReport(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetMessageTrackingReportResponseMessageType)array[0];
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014471 File Offset: 0x00012671
		public void GetMessageTrackingReportAsync(GetMessageTrackingReportRequestType GetMessageTrackingReport1)
		{
			this.GetMessageTrackingReportAsync(GetMessageTrackingReport1, null);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001447C File Offset: 0x0001267C
		public void GetMessageTrackingReportAsync(GetMessageTrackingReportRequestType GetMessageTrackingReport1, object userState)
		{
			if (this.GetMessageTrackingReportOperationCompleted == null)
			{
				this.GetMessageTrackingReportOperationCompleted = new SendOrPostCallback(this.OnGetMessageTrackingReportOperationCompleted);
			}
			base.InvokeAsync("GetMessageTrackingReport", new object[]
			{
				GetMessageTrackingReport1
			}, this.GetMessageTrackingReportOperationCompleted, userState);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000144C4 File Offset: 0x000126C4
		private void OnGetMessageTrackingReportOperationCompleted(object arg)
		{
			if (this.GetMessageTrackingReportCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMessageTrackingReportCompleted(this, new GetMessageTrackingReportCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001450C File Offset: 0x0001270C
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindConversation", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("FindConversationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindConversationResponseMessageType FindConversation([XmlElement("FindConversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindConversationType FindConversation1)
		{
			object[] array = base.Invoke("FindConversation", new object[]
			{
				FindConversation1
			});
			return (FindConversationResponseMessageType)array[0];
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001453C File Offset: 0x0001273C
		public IAsyncResult BeginFindConversation(FindConversationType FindConversation1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindConversation", new object[]
			{
				FindConversation1
			}, callback, asyncState);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00014564 File Offset: 0x00012764
		public FindConversationResponseMessageType EndFindConversation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindConversationResponseMessageType)array[0];
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00014581 File Offset: 0x00012781
		public void FindConversationAsync(FindConversationType FindConversation1)
		{
			this.FindConversationAsync(FindConversation1, null);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001458C File Offset: 0x0001278C
		public void FindConversationAsync(FindConversationType FindConversation1, object userState)
		{
			if (this.FindConversationOperationCompleted == null)
			{
				this.FindConversationOperationCompleted = new SendOrPostCallback(this.OnFindConversationOperationCompleted);
			}
			base.InvokeAsync("FindConversation", new object[]
			{
				FindConversation1
			}, this.FindConversationOperationCompleted, userState);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000145D4 File Offset: 0x000127D4
		private void OnFindConversationOperationCompleted(object arg)
		{
			if (this.FindConversationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindConversationCompleted(this, new FindConversationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001461C File Offset: 0x0001281C
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/ApplyConversationAction", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("ApplyConversationActionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ApplyConversationActionResponseType ApplyConversationAction([XmlElement("ApplyConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] ApplyConversationActionType ApplyConversationAction1)
		{
			object[] array = base.Invoke("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			});
			return (ApplyConversationActionResponseType)array[0];
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001464C File Offset: 0x0001284C
		public IAsyncResult BeginApplyConversationAction(ApplyConversationActionType ApplyConversationAction1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			}, callback, asyncState);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00014674 File Offset: 0x00012874
		public ApplyConversationActionResponseType EndApplyConversationAction(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ApplyConversationActionResponseType)array[0];
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00014691 File Offset: 0x00012891
		public void ApplyConversationActionAsync(ApplyConversationActionType ApplyConversationAction1)
		{
			this.ApplyConversationActionAsync(ApplyConversationAction1, null);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001469C File Offset: 0x0001289C
		public void ApplyConversationActionAsync(ApplyConversationActionType ApplyConversationAction1, object userState)
		{
			if (this.ApplyConversationActionOperationCompleted == null)
			{
				this.ApplyConversationActionOperationCompleted = new SendOrPostCallback(this.OnApplyConversationActionOperationCompleted);
			}
			base.InvokeAsync("ApplyConversationAction", new object[]
			{
				ApplyConversationAction1
			}, this.ApplyConversationActionOperationCompleted, userState);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000146E4 File Offset: 0x000128E4
		private void OnApplyConversationActionOperationCompleted(object arg)
		{
			if (this.ApplyConversationActionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ApplyConversationActionCompleted(this, new ApplyConversationActionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001472C File Offset: 0x0001292C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetConversationItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetConversationItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetConversationItemsResponseType GetConversationItems([XmlElement("GetConversationItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetConversationItemsType GetConversationItems1)
		{
			object[] array = base.Invoke("GetConversationItems", new object[]
			{
				GetConversationItems1
			});
			return (GetConversationItemsResponseType)array[0];
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001475C File Offset: 0x0001295C
		public IAsyncResult BeginGetConversationItems(GetConversationItemsType GetConversationItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetConversationItems", new object[]
			{
				GetConversationItems1
			}, callback, asyncState);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00014784 File Offset: 0x00012984
		public GetConversationItemsResponseType EndGetConversationItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetConversationItemsResponseType)array[0];
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000147A1 File Offset: 0x000129A1
		public void GetConversationItemsAsync(GetConversationItemsType GetConversationItems1)
		{
			this.GetConversationItemsAsync(GetConversationItems1, null);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000147AC File Offset: 0x000129AC
		public void GetConversationItemsAsync(GetConversationItemsType GetConversationItems1, object userState)
		{
			if (this.GetConversationItemsOperationCompleted == null)
			{
				this.GetConversationItemsOperationCompleted = new SendOrPostCallback(this.OnGetConversationItemsOperationCompleted);
			}
			base.InvokeAsync("GetConversationItems", new object[]
			{
				GetConversationItems1
			}, this.GetConversationItemsOperationCompleted, userState);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000147F4 File Offset: 0x000129F4
		private void OnGetConversationItemsOperationCompleted(object arg)
		{
			if (this.GetConversationItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetConversationItemsCompleted(this, new GetConversationItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001483C File Offset: 0x00012A3C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/FindPeople", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("FindPeopleResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindPeopleResponseMessageType FindPeople([XmlElement("FindPeople", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] FindPeopleType FindPeople1)
		{
			object[] array = base.Invoke("FindPeople", new object[]
			{
				FindPeople1
			});
			return (FindPeopleResponseMessageType)array[0];
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001486C File Offset: 0x00012A6C
		public IAsyncResult BeginFindPeople(FindPeopleType FindPeople1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindPeople", new object[]
			{
				FindPeople1
			}, callback, asyncState);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00014894 File Offset: 0x00012A94
		public FindPeopleResponseMessageType EndFindPeople(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FindPeopleResponseMessageType)array[0];
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000148B1 File Offset: 0x00012AB1
		public void FindPeopleAsync(FindPeopleType FindPeople1)
		{
			this.FindPeopleAsync(FindPeople1, null);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000148BC File Offset: 0x00012ABC
		public void FindPeopleAsync(FindPeopleType FindPeople1, object userState)
		{
			if (this.FindPeopleOperationCompleted == null)
			{
				this.FindPeopleOperationCompleted = new SendOrPostCallback(this.OnFindPeopleOperationCompleted);
			}
			base.InvokeAsync("FindPeople", new object[]
			{
				FindPeople1
			}, this.FindPeopleOperationCompleted, userState);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00014904 File Offset: 0x00012B04
		private void OnFindPeopleOperationCompleted(object arg)
		{
			if (this.FindPeopleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindPeopleCompleted(this, new FindPeopleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001494C File Offset: 0x00012B4C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPersona", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetPersonaResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPersonaResponseMessageType GetPersona([XmlElement("GetPersona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPersonaType GetPersona1)
		{
			object[] array = base.Invoke("GetPersona", new object[]
			{
				GetPersona1
			});
			return (GetPersonaResponseMessageType)array[0];
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001497C File Offset: 0x00012B7C
		public IAsyncResult BeginGetPersona(GetPersonaType GetPersona1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPersona", new object[]
			{
				GetPersona1
			}, callback, asyncState);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000149A4 File Offset: 0x00012BA4
		public GetPersonaResponseMessageType EndGetPersona(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPersonaResponseMessageType)array[0];
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000149C1 File Offset: 0x00012BC1
		public void GetPersonaAsync(GetPersonaType GetPersona1)
		{
			this.GetPersonaAsync(GetPersona1, null);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000149CC File Offset: 0x00012BCC
		public void GetPersonaAsync(GetPersonaType GetPersona1, object userState)
		{
			if (this.GetPersonaOperationCompleted == null)
			{
				this.GetPersonaOperationCompleted = new SendOrPostCallback(this.OnGetPersonaOperationCompleted);
			}
			base.InvokeAsync("GetPersona", new object[]
			{
				GetPersona1
			}, this.GetPersonaOperationCompleted, userState);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00014A14 File Offset: 0x00012C14
		private void OnGetPersonaOperationCompleted(object arg)
		{
			if (this.GetPersonaCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPersonaCompleted(this, new GetPersonaCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00014A5C File Offset: 0x00012C5C
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetInboxRulesResponseType GetInboxRules([XmlElement("GetInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetInboxRulesRequestType GetInboxRules1)
		{
			object[] array = base.Invoke("GetInboxRules", new object[]
			{
				GetInboxRules1
			});
			return (GetInboxRulesResponseType)array[0];
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00014A8C File Offset: 0x00012C8C
		public IAsyncResult BeginGetInboxRules(GetInboxRulesRequestType GetInboxRules1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetInboxRules", new object[]
			{
				GetInboxRules1
			}, callback, asyncState);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public GetInboxRulesResponseType EndGetInboxRules(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetInboxRulesResponseType)array[0];
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00014AD1 File Offset: 0x00012CD1
		public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1)
		{
			this.GetInboxRulesAsync(GetInboxRules1, null);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00014ADC File Offset: 0x00012CDC
		public void GetInboxRulesAsync(GetInboxRulesRequestType GetInboxRules1, object userState)
		{
			if (this.GetInboxRulesOperationCompleted == null)
			{
				this.GetInboxRulesOperationCompleted = new SendOrPostCallback(this.OnGetInboxRulesOperationCompleted);
			}
			base.InvokeAsync("GetInboxRules", new object[]
			{
				GetInboxRules1
			}, this.GetInboxRulesOperationCompleted, userState);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00014B24 File Offset: 0x00012D24
		private void OnGetInboxRulesOperationCompleted(object arg)
		{
			if (this.GetInboxRulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetInboxRulesCompleted(this, new GetInboxRulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00014B6C File Offset: 0x00012D6C
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("TimeZoneContext")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UpdateInboxRules", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UpdateInboxRulesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UpdateInboxRulesResponseType UpdateInboxRules([XmlElement("UpdateInboxRules", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UpdateInboxRulesRequestType UpdateInboxRules1)
		{
			object[] array = base.Invoke("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			});
			return (UpdateInboxRulesResponseType)array[0];
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00014B9C File Offset: 0x00012D9C
		public IAsyncResult BeginUpdateInboxRules(UpdateInboxRulesRequestType UpdateInboxRules1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			}, callback, asyncState);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00014BC4 File Offset: 0x00012DC4
		public UpdateInboxRulesResponseType EndUpdateInboxRules(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UpdateInboxRulesResponseType)array[0];
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00014BE1 File Offset: 0x00012DE1
		public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1)
		{
			this.UpdateInboxRulesAsync(UpdateInboxRules1, null);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00014BEC File Offset: 0x00012DEC
		public void UpdateInboxRulesAsync(UpdateInboxRulesRequestType UpdateInboxRules1, object userState)
		{
			if (this.UpdateInboxRulesOperationCompleted == null)
			{
				this.UpdateInboxRulesOperationCompleted = new SendOrPostCallback(this.OnUpdateInboxRulesOperationCompleted);
			}
			base.InvokeAsync("UpdateInboxRules", new object[]
			{
				UpdateInboxRules1
			}, this.UpdateInboxRulesOperationCompleted, userState);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00014C34 File Offset: 0x00012E34
		private void OnUpdateInboxRulesOperationCompleted(object arg)
		{
			if (this.UpdateInboxRulesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateInboxRulesCompleted(this, new UpdateInboxRulesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00014C7C File Offset: 0x00012E7C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetPasswordExpirationDate", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetPasswordExpirationDateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetPasswordExpirationDateResponseMessageType GetPasswordExpirationDate([XmlElement("GetPasswordExpirationDate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetPasswordExpirationDateType GetPasswordExpirationDate1)
		{
			object[] array = base.Invoke("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			});
			return (GetPasswordExpirationDateResponseMessageType)array[0];
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00014CAC File Offset: 0x00012EAC
		public IAsyncResult BeginGetPasswordExpirationDate(GetPasswordExpirationDateType GetPasswordExpirationDate1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			}, callback, asyncState);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00014CD4 File Offset: 0x00012ED4
		public GetPasswordExpirationDateResponseMessageType EndGetPasswordExpirationDate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetPasswordExpirationDateResponseMessageType)array[0];
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00014CF1 File Offset: 0x00012EF1
		public void GetPasswordExpirationDateAsync(GetPasswordExpirationDateType GetPasswordExpirationDate1)
		{
			this.GetPasswordExpirationDateAsync(GetPasswordExpirationDate1, null);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00014CFC File Offset: 0x00012EFC
		public void GetPasswordExpirationDateAsync(GetPasswordExpirationDateType GetPasswordExpirationDate1, object userState)
		{
			if (this.GetPasswordExpirationDateOperationCompleted == null)
			{
				this.GetPasswordExpirationDateOperationCompleted = new SendOrPostCallback(this.OnGetPasswordExpirationDateOperationCompleted);
			}
			base.InvokeAsync("GetPasswordExpirationDate", new object[]
			{
				GetPasswordExpirationDate1
			}, this.GetPasswordExpirationDateOperationCompleted, userState);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00014D44 File Offset: 0x00012F44
		private void OnGetPasswordExpirationDateOperationCompleted(object arg)
		{
			if (this.GetPasswordExpirationDateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPasswordExpirationDateCompleted(this, new GetPasswordExpirationDateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00014D8C File Offset: 0x00012F8C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetSearchableMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetSearchableMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetSearchableMailboxesResponseMessageType GetSearchableMailboxes([XmlElement("GetSearchableMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetSearchableMailboxesType GetSearchableMailboxes1)
		{
			object[] array = base.Invoke("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			});
			return (GetSearchableMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014DBC File Offset: 0x00012FBC
		public IAsyncResult BeginGetSearchableMailboxes(GetSearchableMailboxesType GetSearchableMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014DE4 File Offset: 0x00012FE4
		public GetSearchableMailboxesResponseMessageType EndGetSearchableMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetSearchableMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00014E01 File Offset: 0x00013001
		public void GetSearchableMailboxesAsync(GetSearchableMailboxesType GetSearchableMailboxes1)
		{
			this.GetSearchableMailboxesAsync(GetSearchableMailboxes1, null);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00014E0C File Offset: 0x0001300C
		public void GetSearchableMailboxesAsync(GetSearchableMailboxesType GetSearchableMailboxes1, object userState)
		{
			if (this.GetSearchableMailboxesOperationCompleted == null)
			{
				this.GetSearchableMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetSearchableMailboxesOperationCompleted);
			}
			base.InvokeAsync("GetSearchableMailboxes", new object[]
			{
				GetSearchableMailboxes1
			}, this.GetSearchableMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00014E54 File Offset: 0x00013054
		private void OnGetSearchableMailboxesOperationCompleted(object arg)
		{
			if (this.GetSearchableMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSearchableMailboxesCompleted(this, new GetSearchableMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00014E9C File Offset: 0x0001309C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SearchMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("SearchMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SearchMailboxesResponseType SearchMailboxes([XmlElement("SearchMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SearchMailboxesType SearchMailboxes1)
		{
			object[] array = base.Invoke("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			});
			return (SearchMailboxesResponseType)array[0];
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00014ECC File Offset: 0x000130CC
		public IAsyncResult BeginSearchMailboxes(SearchMailboxesType SearchMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00014EF4 File Offset: 0x000130F4
		public SearchMailboxesResponseType EndSearchMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SearchMailboxesResponseType)array[0];
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014F11 File Offset: 0x00013111
		public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1)
		{
			this.SearchMailboxesAsync(SearchMailboxes1, null);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014F1C File Offset: 0x0001311C
		public void SearchMailboxesAsync(SearchMailboxesType SearchMailboxes1, object userState)
		{
			if (this.SearchMailboxesOperationCompleted == null)
			{
				this.SearchMailboxesOperationCompleted = new SendOrPostCallback(this.OnSearchMailboxesOperationCompleted);
			}
			base.InvokeAsync("SearchMailboxes", new object[]
			{
				SearchMailboxes1
			}, this.SearchMailboxesOperationCompleted, userState);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014F64 File Offset: 0x00013164
		private void OnSearchMailboxesOperationCompleted(object arg)
		{
			if (this.SearchMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SearchMailboxesCompleted(this, new SearchMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014FAC File Offset: 0x000131AC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetDiscoverySearchConfiguration", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("GetDiscoverySearchConfigurationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetDiscoverySearchConfigurationResponseMessageType GetDiscoverySearchConfiguration([XmlElement("GetDiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
		{
			object[] array = base.Invoke("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			});
			return (GetDiscoverySearchConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014FDC File Offset: 0x000131DC
		public IAsyncResult BeginGetDiscoverySearchConfiguration(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			}, callback, asyncState);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00015004 File Offset: 0x00013204
		public GetDiscoverySearchConfigurationResponseMessageType EndGetDiscoverySearchConfiguration(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetDiscoverySearchConfigurationResponseMessageType)array[0];
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00015021 File Offset: 0x00013221
		public void GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1)
		{
			this.GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfiguration1, null);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001502C File Offset: 0x0001322C
		public void GetDiscoverySearchConfigurationAsync(GetDiscoverySearchConfigurationType GetDiscoverySearchConfiguration1, object userState)
		{
			if (this.GetDiscoverySearchConfigurationOperationCompleted == null)
			{
				this.GetDiscoverySearchConfigurationOperationCompleted = new SendOrPostCallback(this.OnGetDiscoverySearchConfigurationOperationCompleted);
			}
			base.InvokeAsync("GetDiscoverySearchConfiguration", new object[]
			{
				GetDiscoverySearchConfiguration1
			}, this.GetDiscoverySearchConfigurationOperationCompleted, userState);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00015074 File Offset: 0x00013274
		private void OnGetDiscoverySearchConfigurationOperationCompleted(object arg)
		{
			if (this.GetDiscoverySearchConfigurationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDiscoverySearchConfigurationCompleted(this, new GetDiscoverySearchConfigurationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000150BC File Offset: 0x000132BC
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("GetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetHoldOnMailboxesResponseMessageType GetHoldOnMailboxes([XmlElement("GetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetHoldOnMailboxesType GetHoldOnMailboxes1)
		{
			object[] array = base.Invoke("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			});
			return (GetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000150EC File Offset: 0x000132EC
		public IAsyncResult BeginGetHoldOnMailboxes(GetHoldOnMailboxesType GetHoldOnMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00015114 File Offset: 0x00013314
		public GetHoldOnMailboxesResponseMessageType EndGetHoldOnMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00015131 File Offset: 0x00013331
		public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1)
		{
			this.GetHoldOnMailboxesAsync(GetHoldOnMailboxes1, null);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001513C File Offset: 0x0001333C
		public void GetHoldOnMailboxesAsync(GetHoldOnMailboxesType GetHoldOnMailboxes1, object userState)
		{
			if (this.GetHoldOnMailboxesOperationCompleted == null)
			{
				this.GetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnGetHoldOnMailboxesOperationCompleted);
			}
			base.InvokeAsync("GetHoldOnMailboxes", new object[]
			{
				GetHoldOnMailboxes1
			}, this.GetHoldOnMailboxesOperationCompleted, userState);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00015184 File Offset: 0x00013384
		private void OnGetHoldOnMailboxesOperationCompleted(object arg)
		{
			if (this.GetHoldOnMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetHoldOnMailboxesCompleted(this, new GetHoldOnMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000151CC File Offset: 0x000133CC
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetHoldOnMailboxes", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[return: XmlElement("SetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetHoldOnMailboxesResponseMessageType SetHoldOnMailboxes([XmlElement("SetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetHoldOnMailboxesType SetHoldOnMailboxes1)
		{
			object[] array = base.Invoke("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			});
			return (SetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000151FC File Offset: 0x000133FC
		public IAsyncResult BeginSetHoldOnMailboxes(SetHoldOnMailboxesType SetHoldOnMailboxes1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			}, callback, asyncState);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00015224 File Offset: 0x00013424
		public SetHoldOnMailboxesResponseMessageType EndSetHoldOnMailboxes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetHoldOnMailboxesResponseMessageType)array[0];
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00015241 File Offset: 0x00013441
		public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1)
		{
			this.SetHoldOnMailboxesAsync(SetHoldOnMailboxes1, null);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001524C File Offset: 0x0001344C
		public void SetHoldOnMailboxesAsync(SetHoldOnMailboxesType SetHoldOnMailboxes1, object userState)
		{
			if (this.SetHoldOnMailboxesOperationCompleted == null)
			{
				this.SetHoldOnMailboxesOperationCompleted = new SendOrPostCallback(this.OnSetHoldOnMailboxesOperationCompleted);
			}
			base.InvokeAsync("SetHoldOnMailboxes", new object[]
			{
				SetHoldOnMailboxes1
			}, this.SetHoldOnMailboxesOperationCompleted, userState);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00015294 File Offset: 0x00013494
		private void OnSetHoldOnMailboxesOperationCompleted(object arg)
		{
			if (this.SetHoldOnMailboxesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetHoldOnMailboxesCompleted(this, new SetHoldOnMailboxesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000152DC File Offset: 0x000134DC
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ManagementRole")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemStatistics", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetNonIndexableItemStatisticsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetNonIndexableItemStatisticsResponseMessageType GetNonIndexableItemStatistics([XmlElement("GetNonIndexableItemStatistics", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
		{
			object[] array = base.Invoke("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			});
			return (GetNonIndexableItemStatisticsResponseMessageType)array[0];
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001530C File Offset: 0x0001350C
		public IAsyncResult BeginGetNonIndexableItemStatistics(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			}, callback, asyncState);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00015334 File Offset: 0x00013534
		public GetNonIndexableItemStatisticsResponseMessageType EndGetNonIndexableItemStatistics(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetNonIndexableItemStatisticsResponseMessageType)array[0];
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00015351 File Offset: 0x00013551
		public void GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1)
		{
			this.GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatistics1, null);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001535C File Offset: 0x0001355C
		public void GetNonIndexableItemStatisticsAsync(GetNonIndexableItemStatisticsType GetNonIndexableItemStatistics1, object userState)
		{
			if (this.GetNonIndexableItemStatisticsOperationCompleted == null)
			{
				this.GetNonIndexableItemStatisticsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemStatisticsOperationCompleted);
			}
			base.InvokeAsync("GetNonIndexableItemStatistics", new object[]
			{
				GetNonIndexableItemStatistics1
			}, this.GetNonIndexableItemStatisticsOperationCompleted, userState);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000153A4 File Offset: 0x000135A4
		private void OnGetNonIndexableItemStatisticsOperationCompleted(object arg)
		{
			if (this.GetNonIndexableItemStatisticsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNonIndexableItemStatisticsCompleted(this, new GetNonIndexableItemStatisticsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000153EC File Offset: 0x000135EC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetNonIndexableItemDetails", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ManagementRole")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetNonIndexableItemDetailsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetNonIndexableItemDetailsResponseMessageType GetNonIndexableItemDetails([XmlElement("GetNonIndexableItemDetails", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
		{
			object[] array = base.Invoke("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			});
			return (GetNonIndexableItemDetailsResponseMessageType)array[0];
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001541C File Offset: 0x0001361C
		public IAsyncResult BeginGetNonIndexableItemDetails(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			}, callback, asyncState);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00015444 File Offset: 0x00013644
		public GetNonIndexableItemDetailsResponseMessageType EndGetNonIndexableItemDetails(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetNonIndexableItemDetailsResponseMessageType)array[0];
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00015461 File Offset: 0x00013661
		public void GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1)
		{
			this.GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetails1, null);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001546C File Offset: 0x0001366C
		public void GetNonIndexableItemDetailsAsync(GetNonIndexableItemDetailsType GetNonIndexableItemDetails1, object userState)
		{
			if (this.GetNonIndexableItemDetailsOperationCompleted == null)
			{
				this.GetNonIndexableItemDetailsOperationCompleted = new SendOrPostCallback(this.OnGetNonIndexableItemDetailsOperationCompleted);
			}
			base.InvokeAsync("GetNonIndexableItemDetails", new object[]
			{
				GetNonIndexableItemDetails1
			}, this.GetNonIndexableItemDetailsOperationCompleted, userState);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000154B4 File Offset: 0x000136B4
		private void OnGetNonIndexableItemDetailsOperationCompleted(object arg)
		{
			if (this.GetNonIndexableItemDetailsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNonIndexableItemDetailsCompleted(this, new GetNonIndexableItemDetailsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000154FC File Offset: 0x000136FC
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAllItemsAsRead", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("MarkAllItemsAsReadResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MarkAllItemsAsReadResponseType MarkAllItemsAsRead([XmlElement("MarkAllItemsAsRead", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAllItemsAsReadType MarkAllItemsAsRead1)
		{
			object[] array = base.Invoke("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			});
			return (MarkAllItemsAsReadResponseType)array[0];
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001552C File Offset: 0x0001372C
		public IAsyncResult BeginMarkAllItemsAsRead(MarkAllItemsAsReadType MarkAllItemsAsRead1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			}, callback, asyncState);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00015554 File Offset: 0x00013754
		public MarkAllItemsAsReadResponseType EndMarkAllItemsAsRead(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MarkAllItemsAsReadResponseType)array[0];
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00015571 File Offset: 0x00013771
		public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1)
		{
			this.MarkAllItemsAsReadAsync(MarkAllItemsAsRead1, null);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001557C File Offset: 0x0001377C
		public void MarkAllItemsAsReadAsync(MarkAllItemsAsReadType MarkAllItemsAsRead1, object userState)
		{
			if (this.MarkAllItemsAsReadOperationCompleted == null)
			{
				this.MarkAllItemsAsReadOperationCompleted = new SendOrPostCallback(this.OnMarkAllItemsAsReadOperationCompleted);
			}
			base.InvokeAsync("MarkAllItemsAsRead", new object[]
			{
				MarkAllItemsAsRead1
			}, this.MarkAllItemsAsReadOperationCompleted, userState);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000155C4 File Offset: 0x000137C4
		private void OnMarkAllItemsAsReadOperationCompleted(object arg)
		{
			if (this.MarkAllItemsAsReadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MarkAllItemsAsReadCompleted(this, new MarkAllItemsAsReadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001560C File Offset: 0x0001380C
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/MarkAsJunk", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("MarkAsJunkResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public MarkAsJunkResponseType MarkAsJunk([XmlElement("MarkAsJunk", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] MarkAsJunkType MarkAsJunk1)
		{
			object[] array = base.Invoke("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			});
			return (MarkAsJunkResponseType)array[0];
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001563C File Offset: 0x0001383C
		public IAsyncResult BeginMarkAsJunk(MarkAsJunkType MarkAsJunk1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			}, callback, asyncState);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00015664 File Offset: 0x00013864
		public MarkAsJunkResponseType EndMarkAsJunk(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MarkAsJunkResponseType)array[0];
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00015681 File Offset: 0x00013881
		public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1)
		{
			this.MarkAsJunkAsync(MarkAsJunk1, null);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001568C File Offset: 0x0001388C
		public void MarkAsJunkAsync(MarkAsJunkType MarkAsJunk1, object userState)
		{
			if (this.MarkAsJunkOperationCompleted == null)
			{
				this.MarkAsJunkOperationCompleted = new SendOrPostCallback(this.OnMarkAsJunkOperationCompleted);
			}
			base.InvokeAsync("MarkAsJunk", new object[]
			{
				MarkAsJunk1
			}, this.MarkAsJunkOperationCompleted, userState);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000156D4 File Offset: 0x000138D4
		private void OnMarkAsJunkOperationCompleted(object arg)
		{
			if (this.MarkAsJunkCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MarkAsJunkCompleted(this, new MarkAsJunkCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001571C File Offset: 0x0001391C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppManifests", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetAppManifestsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAppManifestsResponseType GetAppManifests([XmlElement("GetAppManifests", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppManifestsType GetAppManifests1)
		{
			object[] array = base.Invoke("GetAppManifests", new object[]
			{
				GetAppManifests1
			});
			return (GetAppManifestsResponseType)array[0];
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001574C File Offset: 0x0001394C
		public IAsyncResult BeginGetAppManifests(GetAppManifestsType GetAppManifests1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAppManifests", new object[]
			{
				GetAppManifests1
			}, callback, asyncState);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00015774 File Offset: 0x00013974
		public GetAppManifestsResponseType EndGetAppManifests(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAppManifestsResponseType)array[0];
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00015791 File Offset: 0x00013991
		public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1)
		{
			this.GetAppManifestsAsync(GetAppManifests1, null);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001579C File Offset: 0x0001399C
		public void GetAppManifestsAsync(GetAppManifestsType GetAppManifests1, object userState)
		{
			if (this.GetAppManifestsOperationCompleted == null)
			{
				this.GetAppManifestsOperationCompleted = new SendOrPostCallback(this.OnGetAppManifestsOperationCompleted);
			}
			base.InvokeAsync("GetAppManifests", new object[]
			{
				GetAppManifests1
			}, this.GetAppManifestsOperationCompleted, userState);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000157E4 File Offset: 0x000139E4
		private void OnGetAppManifestsOperationCompleted(object arg)
		{
			if (this.GetAppManifestsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAppManifestsCompleted(this, new GetAppManifestsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001582C File Offset: 0x00013A2C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("AddNewImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddNewImContactToGroupResponseMessageType AddNewImContactToGroup([XmlElement("AddNewImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewImContactToGroupType AddNewImContactToGroup1)
		{
			object[] array = base.Invoke("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			});
			return (AddNewImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001585C File Offset: 0x00013A5C
		public IAsyncResult BeginAddNewImContactToGroup(AddNewImContactToGroupType AddNewImContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00015884 File Offset: 0x00013A84
		public AddNewImContactToGroupResponseMessageType EndAddNewImContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddNewImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000158A1 File Offset: 0x00013AA1
		public void AddNewImContactToGroupAsync(AddNewImContactToGroupType AddNewImContactToGroup1)
		{
			this.AddNewImContactToGroupAsync(AddNewImContactToGroup1, null);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000158AC File Offset: 0x00013AAC
		public void AddNewImContactToGroupAsync(AddNewImContactToGroupType AddNewImContactToGroup1, object userState)
		{
			if (this.AddNewImContactToGroupOperationCompleted == null)
			{
				this.AddNewImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewImContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddNewImContactToGroup", new object[]
			{
				AddNewImContactToGroup1
			}, this.AddNewImContactToGroupOperationCompleted, userState);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000158F4 File Offset: 0x00013AF4
		private void OnAddNewImContactToGroupOperationCompleted(object arg)
		{
			if (this.AddNewImContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddNewImContactToGroupCompleted(this, new AddNewImContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001593C File Offset: 0x00013B3C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddNewTelUriContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("AddNewTelUriContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddNewTelUriContactToGroupResponseMessageType AddNewTelUriContactToGroup([XmlElement("AddNewTelUriContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
		{
			object[] array = base.Invoke("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			});
			return (AddNewTelUriContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001596C File Offset: 0x00013B6C
		public IAsyncResult BeginAddNewTelUriContactToGroup(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00015994 File Offset: 0x00013B94
		public AddNewTelUriContactToGroupResponseMessageType EndAddNewTelUriContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddNewTelUriContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000159B1 File Offset: 0x00013BB1
		public void AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1)
		{
			this.AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroup1, null);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000159BC File Offset: 0x00013BBC
		public void AddNewTelUriContactToGroupAsync(AddNewTelUriContactToGroupType AddNewTelUriContactToGroup1, object userState)
		{
			if (this.AddNewTelUriContactToGroupOperationCompleted == null)
			{
				this.AddNewTelUriContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddNewTelUriContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddNewTelUriContactToGroup", new object[]
			{
				AddNewTelUriContactToGroup1
			}, this.AddNewTelUriContactToGroupOperationCompleted, userState);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00015A04 File Offset: 0x00013C04
		private void OnAddNewTelUriContactToGroupOperationCompleted(object arg)
		{
			if (this.AddNewTelUriContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddNewTelUriContactToGroupCompleted(this, new AddNewTelUriContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015A4C File Offset: 0x00013C4C
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImContactToGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("AddImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddImContactToGroupResponseMessageType AddImContactToGroup([XmlElement("AddImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImContactToGroupType AddImContactToGroup1)
		{
			object[] array = base.Invoke("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			});
			return (AddImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00015A7C File Offset: 0x00013C7C
		public IAsyncResult BeginAddImContactToGroup(AddImContactToGroupType AddImContactToGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public AddImContactToGroupResponseMessageType EndAddImContactToGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddImContactToGroupResponseMessageType)array[0];
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00015AC1 File Offset: 0x00013CC1
		public void AddImContactToGroupAsync(AddImContactToGroupType AddImContactToGroup1)
		{
			this.AddImContactToGroupAsync(AddImContactToGroup1, null);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00015ACC File Offset: 0x00013CCC
		public void AddImContactToGroupAsync(AddImContactToGroupType AddImContactToGroup1, object userState)
		{
			if (this.AddImContactToGroupOperationCompleted == null)
			{
				this.AddImContactToGroupOperationCompleted = new SendOrPostCallback(this.OnAddImContactToGroupOperationCompleted);
			}
			base.InvokeAsync("AddImContactToGroup", new object[]
			{
				AddImContactToGroup1
			}, this.AddImContactToGroupOperationCompleted, userState);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015B14 File Offset: 0x00013D14
		private void OnAddImContactToGroupOperationCompleted(object arg)
		{
			if (this.AddImContactToGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddImContactToGroupCompleted(this, new AddImContactToGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00015B5C File Offset: 0x00013D5C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImContactFromGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("RemoveImContactFromGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveImContactFromGroupResponseMessageType RemoveImContactFromGroup([XmlElement("RemoveImContactFromGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImContactFromGroupType RemoveImContactFromGroup1)
		{
			object[] array = base.Invoke("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			});
			return (RemoveImContactFromGroupResponseMessageType)array[0];
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00015B8C File Offset: 0x00013D8C
		public IAsyncResult BeginRemoveImContactFromGroup(RemoveImContactFromGroupType RemoveImContactFromGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public RemoveImContactFromGroupResponseMessageType EndRemoveImContactFromGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveImContactFromGroupResponseMessageType)array[0];
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015BD1 File Offset: 0x00013DD1
		public void RemoveImContactFromGroupAsync(RemoveImContactFromGroupType RemoveImContactFromGroup1)
		{
			this.RemoveImContactFromGroupAsync(RemoveImContactFromGroup1, null);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00015BDC File Offset: 0x00013DDC
		public void RemoveImContactFromGroupAsync(RemoveImContactFromGroupType RemoveImContactFromGroup1, object userState)
		{
			if (this.RemoveImContactFromGroupOperationCompleted == null)
			{
				this.RemoveImContactFromGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImContactFromGroupOperationCompleted);
			}
			base.InvokeAsync("RemoveImContactFromGroup", new object[]
			{
				RemoveImContactFromGroup1
			}, this.RemoveImContactFromGroupOperationCompleted, userState);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015C24 File Offset: 0x00013E24
		private void OnRemoveImContactFromGroupOperationCompleted(object arg)
		{
			if (this.RemoveImContactFromGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveImContactFromGroupCompleted(this, new RemoveImContactFromGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00015C6C File Offset: 0x00013E6C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("AddImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddImGroupResponseMessageType AddImGroup([XmlElement("AddImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddImGroupType AddImGroup1)
		{
			object[] array = base.Invoke("AddImGroup", new object[]
			{
				AddImGroup1
			});
			return (AddImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00015C9C File Offset: 0x00013E9C
		public IAsyncResult BeginAddImGroup(AddImGroupType AddImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddImGroup", new object[]
			{
				AddImGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015CC4 File Offset: 0x00013EC4
		public AddImGroupResponseMessageType EndAddImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00015CE1 File Offset: 0x00013EE1
		public void AddImGroupAsync(AddImGroupType AddImGroup1)
		{
			this.AddImGroupAsync(AddImGroup1, null);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00015CEC File Offset: 0x00013EEC
		public void AddImGroupAsync(AddImGroupType AddImGroup1, object userState)
		{
			if (this.AddImGroupOperationCompleted == null)
			{
				this.AddImGroupOperationCompleted = new SendOrPostCallback(this.OnAddImGroupOperationCompleted);
			}
			base.InvokeAsync("AddImGroup", new object[]
			{
				AddImGroup1
			}, this.AddImGroupOperationCompleted, userState);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00015D34 File Offset: 0x00013F34
		private void OnAddImGroupOperationCompleted(object arg)
		{
			if (this.AddImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddImGroupCompleted(this, new AddImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015D7C File Offset: 0x00013F7C
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/AddDistributionGroupToImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("AddDistributionGroupToImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public AddDistributionGroupToImListResponseMessageType AddDistributionGroupToImList([XmlElement("AddDistributionGroupToImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] AddDistributionGroupToImListType AddDistributionGroupToImList1)
		{
			object[] array = base.Invoke("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			});
			return (AddDistributionGroupToImListResponseMessageType)array[0];
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00015DAC File Offset: 0x00013FAC
		public IAsyncResult BeginAddDistributionGroupToImList(AddDistributionGroupToImListType AddDistributionGroupToImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			}, callback, asyncState);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00015DD4 File Offset: 0x00013FD4
		public AddDistributionGroupToImListResponseMessageType EndAddDistributionGroupToImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AddDistributionGroupToImListResponseMessageType)array[0];
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015DF1 File Offset: 0x00013FF1
		public void AddDistributionGroupToImListAsync(AddDistributionGroupToImListType AddDistributionGroupToImList1)
		{
			this.AddDistributionGroupToImListAsync(AddDistributionGroupToImList1, null);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015DFC File Offset: 0x00013FFC
		public void AddDistributionGroupToImListAsync(AddDistributionGroupToImListType AddDistributionGroupToImList1, object userState)
		{
			if (this.AddDistributionGroupToImListOperationCompleted == null)
			{
				this.AddDistributionGroupToImListOperationCompleted = new SendOrPostCallback(this.OnAddDistributionGroupToImListOperationCompleted);
			}
			base.InvokeAsync("AddDistributionGroupToImList", new object[]
			{
				AddDistributionGroupToImList1
			}, this.AddDistributionGroupToImListOperationCompleted, userState);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00015E44 File Offset: 0x00014044
		private void OnAddDistributionGroupToImListOperationCompleted(object arg)
		{
			if (this.AddDistributionGroupToImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddDistributionGroupToImListCompleted(this, new AddDistributionGroupToImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00015E8C File Offset: 0x0001408C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItemList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetImItemListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetImItemListResponseMessageType GetImItemList([XmlElement("GetImItemList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemListType GetImItemList1)
		{
			object[] array = base.Invoke("GetImItemList", new object[]
			{
				GetImItemList1
			});
			return (GetImItemListResponseMessageType)array[0];
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015EBC File Offset: 0x000140BC
		public IAsyncResult BeginGetImItemList(GetImItemListType GetImItemList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetImItemList", new object[]
			{
				GetImItemList1
			}, callback, asyncState);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015EE4 File Offset: 0x000140E4
		public GetImItemListResponseMessageType EndGetImItemList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetImItemListResponseMessageType)array[0];
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015F01 File Offset: 0x00014101
		public void GetImItemListAsync(GetImItemListType GetImItemList1)
		{
			this.GetImItemListAsync(GetImItemList1, null);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015F0C File Offset: 0x0001410C
		public void GetImItemListAsync(GetImItemListType GetImItemList1, object userState)
		{
			if (this.GetImItemListOperationCompleted == null)
			{
				this.GetImItemListOperationCompleted = new SendOrPostCallback(this.OnGetImItemListOperationCompleted);
			}
			base.InvokeAsync("GetImItemList", new object[]
			{
				GetImItemList1
			}, this.GetImItemListOperationCompleted, userState);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00015F54 File Offset: 0x00014154
		private void OnGetImItemListOperationCompleted(object arg)
		{
			if (this.GetImItemListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetImItemListCompleted(this, new GetImItemListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00015F9C File Offset: 0x0001419C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetImItems", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetImItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetImItemsResponseMessageType GetImItems([XmlElement("GetImItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetImItemsType GetImItems1)
		{
			object[] array = base.Invoke("GetImItems", new object[]
			{
				GetImItems1
			});
			return (GetImItemsResponseMessageType)array[0];
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015FCC File Offset: 0x000141CC
		public IAsyncResult BeginGetImItems(GetImItemsType GetImItems1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetImItems", new object[]
			{
				GetImItems1
			}, callback, asyncState);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00015FF4 File Offset: 0x000141F4
		public GetImItemsResponseMessageType EndGetImItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetImItemsResponseMessageType)array[0];
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00016011 File Offset: 0x00014211
		public void GetImItemsAsync(GetImItemsType GetImItems1)
		{
			this.GetImItemsAsync(GetImItems1, null);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001601C File Offset: 0x0001421C
		public void GetImItemsAsync(GetImItemsType GetImItems1, object userState)
		{
			if (this.GetImItemsOperationCompleted == null)
			{
				this.GetImItemsOperationCompleted = new SendOrPostCallback(this.OnGetImItemsOperationCompleted);
			}
			base.InvokeAsync("GetImItems", new object[]
			{
				GetImItems1
			}, this.GetImItemsOperationCompleted, userState);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00016064 File Offset: 0x00014264
		private void OnGetImItemsOperationCompleted(object arg)
		{
			if (this.GetImItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetImItemsCompleted(this, new GetImItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000160AC File Offset: 0x000142AC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveContactFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("RemoveContactFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveContactFromImListResponseMessageType RemoveContactFromImList([XmlElement("RemoveContactFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveContactFromImListType RemoveContactFromImList1)
		{
			object[] array = base.Invoke("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			});
			return (RemoveContactFromImListResponseMessageType)array[0];
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000160DC File Offset: 0x000142DC
		public IAsyncResult BeginRemoveContactFromImList(RemoveContactFromImListType RemoveContactFromImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			}, callback, asyncState);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00016104 File Offset: 0x00014304
		public RemoveContactFromImListResponseMessageType EndRemoveContactFromImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveContactFromImListResponseMessageType)array[0];
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00016121 File Offset: 0x00014321
		public void RemoveContactFromImListAsync(RemoveContactFromImListType RemoveContactFromImList1)
		{
			this.RemoveContactFromImListAsync(RemoveContactFromImList1, null);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001612C File Offset: 0x0001432C
		public void RemoveContactFromImListAsync(RemoveContactFromImListType RemoveContactFromImList1, object userState)
		{
			if (this.RemoveContactFromImListOperationCompleted == null)
			{
				this.RemoveContactFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveContactFromImListOperationCompleted);
			}
			base.InvokeAsync("RemoveContactFromImList", new object[]
			{
				RemoveContactFromImList1
			}, this.RemoveContactFromImListOperationCompleted, userState);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00016174 File Offset: 0x00014374
		private void OnRemoveContactFromImListOperationCompleted(object arg)
		{
			if (this.RemoveContactFromImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveContactFromImListCompleted(this, new RemoveContactFromImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000161BC File Offset: 0x000143BC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveDistributionGroupFromImList", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("RemoveDistributionGroupFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveDistributionGroupFromImListResponseMessageType RemoveDistributionGroupFromImList([XmlElement("RemoveDistributionGroupFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
		{
			object[] array = base.Invoke("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			});
			return (RemoveDistributionGroupFromImListResponseMessageType)array[0];
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000161EC File Offset: 0x000143EC
		public IAsyncResult BeginRemoveDistributionGroupFromImList(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			}, callback, asyncState);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00016214 File Offset: 0x00014414
		public RemoveDistributionGroupFromImListResponseMessageType EndRemoveDistributionGroupFromImList(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveDistributionGroupFromImListResponseMessageType)array[0];
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00016231 File Offset: 0x00014431
		public void RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1)
		{
			this.RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImList1, null);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001623C File Offset: 0x0001443C
		public void RemoveDistributionGroupFromImListAsync(RemoveDistributionGroupFromImListType RemoveDistributionGroupFromImList1, object userState)
		{
			if (this.RemoveDistributionGroupFromImListOperationCompleted == null)
			{
				this.RemoveDistributionGroupFromImListOperationCompleted = new SendOrPostCallback(this.OnRemoveDistributionGroupFromImListOperationCompleted);
			}
			base.InvokeAsync("RemoveDistributionGroupFromImList", new object[]
			{
				RemoveDistributionGroupFromImList1
			}, this.RemoveDistributionGroupFromImListOperationCompleted, userState);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00016284 File Offset: 0x00014484
		private void OnRemoveDistributionGroupFromImListOperationCompleted(object arg)
		{
			if (this.RemoveDistributionGroupFromImListCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveDistributionGroupFromImListCompleted(this, new RemoveDistributionGroupFromImListCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000162CC File Offset: 0x000144CC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/RemoveImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ExchangeImpersonation")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("RemoveImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RemoveImGroupResponseMessageType RemoveImGroup([XmlElement("RemoveImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] RemoveImGroupType RemoveImGroup1)
		{
			object[] array = base.Invoke("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			});
			return (RemoveImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000162FC File Offset: 0x000144FC
		public IAsyncResult BeginRemoveImGroup(RemoveImGroupType RemoveImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00016324 File Offset: 0x00014524
		public RemoveImGroupResponseMessageType EndRemoveImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RemoveImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00016341 File Offset: 0x00014541
		public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1)
		{
			this.RemoveImGroupAsync(RemoveImGroup1, null);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001634C File Offset: 0x0001454C
		public void RemoveImGroupAsync(RemoveImGroupType RemoveImGroup1, object userState)
		{
			if (this.RemoveImGroupOperationCompleted == null)
			{
				this.RemoveImGroupOperationCompleted = new SendOrPostCallback(this.OnRemoveImGroupOperationCompleted);
			}
			base.InvokeAsync("RemoveImGroup", new object[]
			{
				RemoveImGroup1
			}, this.RemoveImGroupOperationCompleted, userState);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00016394 File Offset: 0x00014594
		private void OnRemoveImGroupOperationCompleted(object arg)
		{
			if (this.RemoveImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveImGroupCompleted(this, new RemoveImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000163DC File Offset: 0x000145DC
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImGroup", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("MailboxCulture")]
		[return: XmlElement("SetImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetImGroupResponseMessageType SetImGroup([XmlElement("SetImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImGroupType SetImGroup1)
		{
			object[] array = base.Invoke("SetImGroup", new object[]
			{
				SetImGroup1
			});
			return (SetImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001640C File Offset: 0x0001460C
		public IAsyncResult BeginSetImGroup(SetImGroupType SetImGroup1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetImGroup", new object[]
			{
				SetImGroup1
			}, callback, asyncState);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00016434 File Offset: 0x00014634
		public SetImGroupResponseMessageType EndSetImGroup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetImGroupResponseMessageType)array[0];
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00016451 File Offset: 0x00014651
		public void SetImGroupAsync(SetImGroupType SetImGroup1)
		{
			this.SetImGroupAsync(SetImGroup1, null);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001645C File Offset: 0x0001465C
		public void SetImGroupAsync(SetImGroupType SetImGroup1, object userState)
		{
			if (this.SetImGroupOperationCompleted == null)
			{
				this.SetImGroupOperationCompleted = new SendOrPostCallback(this.OnSetImGroupOperationCompleted);
			}
			base.InvokeAsync("SetImGroup", new object[]
			{
				SetImGroup1
			}, this.SetImGroupOperationCompleted, userState);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000164A4 File Offset: 0x000146A4
		private void OnSetImGroupOperationCompleted(object arg)
		{
			if (this.SetImGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetImGroupCompleted(this, new SetImGroupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000164EC File Offset: 0x000146EC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/SetImListMigrationCompleted", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("MailboxCulture")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("ExchangeImpersonation")]
		[return: XmlElement("SetImListMigrationCompletedResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SetImListMigrationCompletedResponseMessageType SetImListMigrationCompleted([XmlElement("SetImListMigrationCompleted", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] SetImListMigrationCompletedType SetImListMigrationCompleted1)
		{
			object[] array = base.Invoke("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			});
			return (SetImListMigrationCompletedResponseMessageType)array[0];
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001651C File Offset: 0x0001471C
		public IAsyncResult BeginSetImListMigrationCompleted(SetImListMigrationCompletedType SetImListMigrationCompleted1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			}, callback, asyncState);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00016544 File Offset: 0x00014744
		public SetImListMigrationCompletedResponseMessageType EndSetImListMigrationCompleted(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SetImListMigrationCompletedResponseMessageType)array[0];
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00016561 File Offset: 0x00014761
		public void SetImListMigrationCompletedAsync(SetImListMigrationCompletedType SetImListMigrationCompleted1)
		{
			this.SetImListMigrationCompletedAsync(SetImListMigrationCompleted1, null);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001656C File Offset: 0x0001476C
		public void SetImListMigrationCompletedAsync(SetImListMigrationCompletedType SetImListMigrationCompleted1, object userState)
		{
			if (this.SetImListMigrationCompletedOperationCompleted == null)
			{
				this.SetImListMigrationCompletedOperationCompleted = new SendOrPostCallback(this.OnSetImListMigrationCompletedOperationCompleted);
			}
			base.InvokeAsync("SetImListMigrationCompleted", new object[]
			{
				SetImListMigrationCompleted1
			}, this.SetImListMigrationCompletedOperationCompleted, userState);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000165B4 File Offset: 0x000147B4
		private void OnSetImListMigrationCompletedOperationCompleted(object arg)
		{
			if (this.SetImListMigrationCompletedCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetImListMigrationCompletedCompleted(this, new SetImListMigrationCompletedCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000165FC File Offset: 0x000147FC
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserRetentionPolicyTags", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[return: XmlElement("GetUserRetentionPolicyTagsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserRetentionPolicyTagsResponseMessageType GetUserRetentionPolicyTags([XmlElement("GetUserRetentionPolicyTags", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
		{
			object[] array = base.Invoke("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			});
			return (GetUserRetentionPolicyTagsResponseMessageType)array[0];
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001662C File Offset: 0x0001482C
		public IAsyncResult BeginGetUserRetentionPolicyTags(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			}, callback, asyncState);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00016654 File Offset: 0x00014854
		public GetUserRetentionPolicyTagsResponseMessageType EndGetUserRetentionPolicyTags(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserRetentionPolicyTagsResponseMessageType)array[0];
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00016671 File Offset: 0x00014871
		public void GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1)
		{
			this.GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTags1, null);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001667C File Offset: 0x0001487C
		public void GetUserRetentionPolicyTagsAsync(GetUserRetentionPolicyTagsType GetUserRetentionPolicyTags1, object userState)
		{
			if (this.GetUserRetentionPolicyTagsOperationCompleted == null)
			{
				this.GetUserRetentionPolicyTagsOperationCompleted = new SendOrPostCallback(this.OnGetUserRetentionPolicyTagsOperationCompleted);
			}
			base.InvokeAsync("GetUserRetentionPolicyTags", new object[]
			{
				GetUserRetentionPolicyTags1
			}, this.GetUserRetentionPolicyTagsOperationCompleted, userState);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000166C4 File Offset: 0x000148C4
		private void OnGetUserRetentionPolicyTagsOperationCompleted(object arg)
		{
			if (this.GetUserRetentionPolicyTagsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserRetentionPolicyTagsCompleted(this, new GetUserRetentionPolicyTagsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001670C File Offset: 0x0001490C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/InstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("InstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public InstallAppResponseType InstallApp([XmlElement("InstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] InstallAppType InstallApp1)
		{
			object[] array = base.Invoke("InstallApp", new object[]
			{
				InstallApp1
			});
			return (InstallAppResponseType)array[0];
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001673C File Offset: 0x0001493C
		public IAsyncResult BeginInstallApp(InstallAppType InstallApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("InstallApp", new object[]
			{
				InstallApp1
			}, callback, asyncState);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016764 File Offset: 0x00014964
		public InstallAppResponseType EndInstallApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (InstallAppResponseType)array[0];
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016781 File Offset: 0x00014981
		public void InstallAppAsync(InstallAppType InstallApp1)
		{
			this.InstallAppAsync(InstallApp1, null);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001678C File Offset: 0x0001498C
		public void InstallAppAsync(InstallAppType InstallApp1, object userState)
		{
			if (this.InstallAppOperationCompleted == null)
			{
				this.InstallAppOperationCompleted = new SendOrPostCallback(this.OnInstallAppOperationCompleted);
			}
			base.InvokeAsync("InstallApp", new object[]
			{
				InstallApp1
			}, this.InstallAppOperationCompleted, userState);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000167D4 File Offset: 0x000149D4
		private void OnInstallAppOperationCompleted(object arg)
		{
			if (this.InstallAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.InstallAppCompleted(this, new InstallAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001681C File Offset: 0x00014A1C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/UninstallApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("UninstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public UninstallAppResponseType UninstallApp([XmlElement("UninstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] UninstallAppType UninstallApp1)
		{
			object[] array = base.Invoke("UninstallApp", new object[]
			{
				UninstallApp1
			});
			return (UninstallAppResponseType)array[0];
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001684C File Offset: 0x00014A4C
		public IAsyncResult BeginUninstallApp(UninstallAppType UninstallApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UninstallApp", new object[]
			{
				UninstallApp1
			}, callback, asyncState);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00016874 File Offset: 0x00014A74
		public UninstallAppResponseType EndUninstallApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (UninstallAppResponseType)array[0];
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00016891 File Offset: 0x00014A91
		public void UninstallAppAsync(UninstallAppType UninstallApp1)
		{
			this.UninstallAppAsync(UninstallApp1, null);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001689C File Offset: 0x00014A9C
		public void UninstallAppAsync(UninstallAppType UninstallApp1, object userState)
		{
			if (this.UninstallAppOperationCompleted == null)
			{
				this.UninstallAppOperationCompleted = new SendOrPostCallback(this.OnUninstallAppOperationCompleted);
			}
			base.InvokeAsync("UninstallApp", new object[]
			{
				UninstallApp1
			}, this.UninstallAppOperationCompleted, userState);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000168E4 File Offset: 0x00014AE4
		private void OnUninstallAppOperationCompleted(object arg)
		{
			if (this.UninstallAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UninstallAppCompleted(this, new UninstallAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001692C File Offset: 0x00014B2C
		[SoapHeader("RequestServerVersionValue")]
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/DisableApp", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("DisableAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DisableAppResponseType DisableApp([XmlElement("DisableApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] DisableAppType DisableApp1)
		{
			object[] array = base.Invoke("DisableApp", new object[]
			{
				DisableApp1
			});
			return (DisableAppResponseType)array[0];
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001695C File Offset: 0x00014B5C
		public IAsyncResult BeginDisableApp(DisableAppType DisableApp1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DisableApp", new object[]
			{
				DisableApp1
			}, callback, asyncState);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00016984 File Offset: 0x00014B84
		public DisableAppResponseType EndDisableApp(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DisableAppResponseType)array[0];
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000169A1 File Offset: 0x00014BA1
		public void DisableAppAsync(DisableAppType DisableApp1)
		{
			this.DisableAppAsync(DisableApp1, null);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000169AC File Offset: 0x00014BAC
		public void DisableAppAsync(DisableAppType DisableApp1, object userState)
		{
			if (this.DisableAppOperationCompleted == null)
			{
				this.DisableAppOperationCompleted = new SendOrPostCallback(this.OnDisableAppOperationCompleted);
			}
			base.InvokeAsync("DisableApp", new object[]
			{
				DisableApp1
			}, this.DisableAppOperationCompleted, userState);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000169F4 File Offset: 0x00014BF4
		private void OnDisableAppOperationCompleted(object arg)
		{
			if (this.DisableAppCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DisableAppCompleted(this, new DisableAppCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00016A3C File Offset: 0x00014C3C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetAppMarketplaceUrl", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[SoapHeader("RequestServerVersionValue")]
		[return: XmlElement("GetAppMarketplaceUrlResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetAppMarketplaceUrlResponseMessageType GetAppMarketplaceUrl([XmlElement("GetAppMarketplaceUrl", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
		{
			object[] array = base.Invoke("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			});
			return (GetAppMarketplaceUrlResponseMessageType)array[0];
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016A6C File Offset: 0x00014C6C
		public IAsyncResult BeginGetAppMarketplaceUrl(GetAppMarketplaceUrlType GetAppMarketplaceUrl1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			}, callback, asyncState);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00016A94 File Offset: 0x00014C94
		public GetAppMarketplaceUrlResponseMessageType EndGetAppMarketplaceUrl(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetAppMarketplaceUrlResponseMessageType)array[0];
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00016AB1 File Offset: 0x00014CB1
		public void GetAppMarketplaceUrlAsync(GetAppMarketplaceUrlType GetAppMarketplaceUrl1)
		{
			this.GetAppMarketplaceUrlAsync(GetAppMarketplaceUrl1, null);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00016ABC File Offset: 0x00014CBC
		public void GetAppMarketplaceUrlAsync(GetAppMarketplaceUrlType GetAppMarketplaceUrl1, object userState)
		{
			if (this.GetAppMarketplaceUrlOperationCompleted == null)
			{
				this.GetAppMarketplaceUrlOperationCompleted = new SendOrPostCallback(this.OnGetAppMarketplaceUrlOperationCompleted);
			}
			base.InvokeAsync("GetAppMarketplaceUrl", new object[]
			{
				GetAppMarketplaceUrl1
			}, this.GetAppMarketplaceUrlOperationCompleted, userState);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00016B04 File Offset: 0x00014D04
		private void OnGetAppMarketplaceUrlOperationCompleted(object arg)
		{
			if (this.GetAppMarketplaceUrlCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAppMarketplaceUrlCompleted(this, new GetAppMarketplaceUrlCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00016B4C File Offset: 0x00014D4C
		[SoapHeader("ServerVersionInfoValue", Direction = SoapHeaderDirection.Out)]
		[SoapHeader("RequestServerVersionValue")]
		[SoapDocumentMethod("http://schemas.microsoft.com/exchange/services/2006/messages/GetUserPhoto", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
		[return: XmlElement("GetUserPhotoResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetUserPhotoResponseMessageType GetUserPhoto([XmlElement("GetUserPhoto", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")] GetUserPhotoType GetUserPhoto1)
		{
			object[] array = base.Invoke("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			});
			return (GetUserPhotoResponseMessageType)array[0];
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00016B7C File Offset: 0x00014D7C
		public IAsyncResult BeginGetUserPhoto(GetUserPhotoType GetUserPhoto1, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			}, callback, asyncState);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00016BA4 File Offset: 0x00014DA4
		public GetUserPhotoResponseMessageType EndGetUserPhoto(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GetUserPhotoResponseMessageType)array[0];
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00016BC1 File Offset: 0x00014DC1
		public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1)
		{
			this.GetUserPhotoAsync(GetUserPhoto1, null);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00016BCC File Offset: 0x00014DCC
		public void GetUserPhotoAsync(GetUserPhotoType GetUserPhoto1, object userState)
		{
			if (this.GetUserPhotoOperationCompleted == null)
			{
				this.GetUserPhotoOperationCompleted = new SendOrPostCallback(this.OnGetUserPhotoOperationCompleted);
			}
			base.InvokeAsync("GetUserPhoto", new object[]
			{
				GetUserPhoto1
			}, this.GetUserPhotoOperationCompleted, userState);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00016C14 File Offset: 0x00014E14
		private void OnGetUserPhotoOperationCompleted(object arg)
		{
			if (this.GetUserPhotoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetUserPhotoCompleted(this, new GetUserPhotoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00016C59 File Offset: 0x00014E59
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x040000E9 RID: 233
		private ExchangeImpersonationType exchangeImpersonationField;

		// Token: 0x040000EA RID: 234
		private MailboxCultureType mailboxCultureField;

		// Token: 0x040000EB RID: 235
		private RequestServerVersion requestServerVersionValueField;

		// Token: 0x040000EC RID: 236
		private ServerVersionInfo serverVersionInfoValueField;

		// Token: 0x040000ED RID: 237
		private SendOrPostCallback ResolveNamesOperationCompleted;

		// Token: 0x040000EE RID: 238
		private SendOrPostCallback ExpandDLOperationCompleted;

		// Token: 0x040000EF RID: 239
		private SendOrPostCallback GetServerTimeZonesOperationCompleted;

		// Token: 0x040000F0 RID: 240
		private TimeZoneContextType timeZoneContextField;

		// Token: 0x040000F1 RID: 241
		private ManagementRoleType managementRoleField;

		// Token: 0x040000F2 RID: 242
		private SendOrPostCallback FindFolderOperationCompleted;

		// Token: 0x040000F3 RID: 243
		private DateTimePrecisionType dateTimePrecisionField;

		// Token: 0x040000F4 RID: 244
		private SendOrPostCallback FindItemOperationCompleted;

		// Token: 0x040000F5 RID: 245
		private SendOrPostCallback GetFolderOperationCompleted;

		// Token: 0x040000F6 RID: 246
		private SendOrPostCallback UploadItemsOperationCompleted;

		// Token: 0x040000F7 RID: 247
		private SendOrPostCallback ExportItemsOperationCompleted;

		// Token: 0x040000F8 RID: 248
		private SendOrPostCallback ConvertIdOperationCompleted;

		// Token: 0x040000F9 RID: 249
		private SendOrPostCallback CreateFolderOperationCompleted;

		// Token: 0x040000FA RID: 250
		private SendOrPostCallback CreateFolderPathOperationCompleted;

		// Token: 0x040000FB RID: 251
		private SendOrPostCallback DeleteFolderOperationCompleted;

		// Token: 0x040000FC RID: 252
		private SendOrPostCallback EmptyFolderOperationCompleted;

		// Token: 0x040000FD RID: 253
		private SendOrPostCallback UpdateFolderOperationCompleted;

		// Token: 0x040000FE RID: 254
		private SendOrPostCallback MoveFolderOperationCompleted;

		// Token: 0x040000FF RID: 255
		private SendOrPostCallback CopyFolderOperationCompleted;

		// Token: 0x04000100 RID: 256
		private SendOrPostCallback SubscribeOperationCompleted;

		// Token: 0x04000101 RID: 257
		private SendOrPostCallback UnsubscribeOperationCompleted;

		// Token: 0x04000102 RID: 258
		private SendOrPostCallback GetEventsOperationCompleted;

		// Token: 0x04000103 RID: 259
		private SendOrPostCallback GetStreamingEventsOperationCompleted;

		// Token: 0x04000104 RID: 260
		private SendOrPostCallback SyncFolderHierarchyOperationCompleted;

		// Token: 0x04000105 RID: 261
		private SendOrPostCallback SyncFolderItemsOperationCompleted;

		// Token: 0x04000106 RID: 262
		private SendOrPostCallback CreateManagedFolderOperationCompleted;

		// Token: 0x04000107 RID: 263
		private SendOrPostCallback GetItemOperationCompleted;

		// Token: 0x04000108 RID: 264
		private SendOrPostCallback CreateItemOperationCompleted;

		// Token: 0x04000109 RID: 265
		private SendOrPostCallback DeleteItemOperationCompleted;

		// Token: 0x0400010A RID: 266
		private SendOrPostCallback UpdateItemOperationCompleted;

		// Token: 0x0400010B RID: 267
		private SendOrPostCallback UpdateItemInRecoverableItemsOperationCompleted;

		// Token: 0x0400010C RID: 268
		private SendOrPostCallback SendItemOperationCompleted;

		// Token: 0x0400010D RID: 269
		private SendOrPostCallback MoveItemOperationCompleted;

		// Token: 0x0400010E RID: 270
		private SendOrPostCallback CopyItemOperationCompleted;

		// Token: 0x0400010F RID: 271
		private SendOrPostCallback ArchiveItemOperationCompleted;

		// Token: 0x04000110 RID: 272
		private SendOrPostCallback CreateAttachmentOperationCompleted;

		// Token: 0x04000111 RID: 273
		private SendOrPostCallback DeleteAttachmentOperationCompleted;

		// Token: 0x04000112 RID: 274
		private SendOrPostCallback GetAttachmentOperationCompleted;

		// Token: 0x04000113 RID: 275
		private SendOrPostCallback GetClientAccessTokenOperationCompleted;

		// Token: 0x04000114 RID: 276
		private SendOrPostCallback GetDelegateOperationCompleted;

		// Token: 0x04000115 RID: 277
		private SendOrPostCallback AddDelegateOperationCompleted;

		// Token: 0x04000116 RID: 278
		private SendOrPostCallback RemoveDelegateOperationCompleted;

		// Token: 0x04000117 RID: 279
		private SendOrPostCallback UpdateDelegateOperationCompleted;

		// Token: 0x04000118 RID: 280
		private SendOrPostCallback CreateUserConfigurationOperationCompleted;

		// Token: 0x04000119 RID: 281
		private SendOrPostCallback DeleteUserConfigurationOperationCompleted;

		// Token: 0x0400011A RID: 282
		private SendOrPostCallback GetUserConfigurationOperationCompleted;

		// Token: 0x0400011B RID: 283
		private SendOrPostCallback UpdateUserConfigurationOperationCompleted;

		// Token: 0x0400011C RID: 284
		private SendOrPostCallback GetUserAvailabilityOperationCompleted;

		// Token: 0x0400011D RID: 285
		private SendOrPostCallback GetUserOofSettingsOperationCompleted;

		// Token: 0x0400011E RID: 286
		private SendOrPostCallback SetUserOofSettingsOperationCompleted;

		// Token: 0x0400011F RID: 287
		private SendOrPostCallback GetServiceConfigurationOperationCompleted;

		// Token: 0x04000120 RID: 288
		private SendOrPostCallback GetMailTipsOperationCompleted;

		// Token: 0x04000121 RID: 289
		private SendOrPostCallback PlayOnPhoneOperationCompleted;

		// Token: 0x04000122 RID: 290
		private SendOrPostCallback GetPhoneCallInformationOperationCompleted;

		// Token: 0x04000123 RID: 291
		private SendOrPostCallback DisconnectPhoneCallOperationCompleted;

		// Token: 0x04000124 RID: 292
		private SendOrPostCallback GetSharingMetadataOperationCompleted;

		// Token: 0x04000125 RID: 293
		private SendOrPostCallback RefreshSharingFolderOperationCompleted;

		// Token: 0x04000126 RID: 294
		private SendOrPostCallback GetSharingFolderOperationCompleted;

		// Token: 0x04000127 RID: 295
		private SendOrPostCallback SetTeamMailboxOperationCompleted;

		// Token: 0x04000128 RID: 296
		private SendOrPostCallback UnpinTeamMailboxOperationCompleted;

		// Token: 0x04000129 RID: 297
		private SendOrPostCallback GetRoomListsOperationCompleted;

		// Token: 0x0400012A RID: 298
		private SendOrPostCallback GetRoomsOperationCompleted;

		// Token: 0x0400012B RID: 299
		private SendOrPostCallback FindMessageTrackingReportOperationCompleted;

		// Token: 0x0400012C RID: 300
		private SendOrPostCallback GetMessageTrackingReportOperationCompleted;

		// Token: 0x0400012D RID: 301
		private SendOrPostCallback FindConversationOperationCompleted;

		// Token: 0x0400012E RID: 302
		private SendOrPostCallback ApplyConversationActionOperationCompleted;

		// Token: 0x0400012F RID: 303
		private SendOrPostCallback GetConversationItemsOperationCompleted;

		// Token: 0x04000130 RID: 304
		private SendOrPostCallback FindPeopleOperationCompleted;

		// Token: 0x04000131 RID: 305
		private SendOrPostCallback GetPersonaOperationCompleted;

		// Token: 0x04000132 RID: 306
		private SendOrPostCallback GetInboxRulesOperationCompleted;

		// Token: 0x04000133 RID: 307
		private SendOrPostCallback UpdateInboxRulesOperationCompleted;

		// Token: 0x04000134 RID: 308
		private SendOrPostCallback GetPasswordExpirationDateOperationCompleted;

		// Token: 0x04000135 RID: 309
		private SendOrPostCallback GetSearchableMailboxesOperationCompleted;

		// Token: 0x04000136 RID: 310
		private SendOrPostCallback SearchMailboxesOperationCompleted;

		// Token: 0x04000137 RID: 311
		private SendOrPostCallback GetDiscoverySearchConfigurationOperationCompleted;

		// Token: 0x04000138 RID: 312
		private SendOrPostCallback GetHoldOnMailboxesOperationCompleted;

		// Token: 0x04000139 RID: 313
		private SendOrPostCallback SetHoldOnMailboxesOperationCompleted;

		// Token: 0x0400013A RID: 314
		private SendOrPostCallback GetNonIndexableItemStatisticsOperationCompleted;

		// Token: 0x0400013B RID: 315
		private SendOrPostCallback GetNonIndexableItemDetailsOperationCompleted;

		// Token: 0x0400013C RID: 316
		private SendOrPostCallback MarkAllItemsAsReadOperationCompleted;

		// Token: 0x0400013D RID: 317
		private SendOrPostCallback MarkAsJunkOperationCompleted;

		// Token: 0x0400013E RID: 318
		private SendOrPostCallback GetAppManifestsOperationCompleted;

		// Token: 0x0400013F RID: 319
		private SendOrPostCallback AddNewImContactToGroupOperationCompleted;

		// Token: 0x04000140 RID: 320
		private SendOrPostCallback AddNewTelUriContactToGroupOperationCompleted;

		// Token: 0x04000141 RID: 321
		private SendOrPostCallback AddImContactToGroupOperationCompleted;

		// Token: 0x04000142 RID: 322
		private SendOrPostCallback RemoveImContactFromGroupOperationCompleted;

		// Token: 0x04000143 RID: 323
		private SendOrPostCallback AddImGroupOperationCompleted;

		// Token: 0x04000144 RID: 324
		private SendOrPostCallback AddDistributionGroupToImListOperationCompleted;

		// Token: 0x04000145 RID: 325
		private SendOrPostCallback GetImItemListOperationCompleted;

		// Token: 0x04000146 RID: 326
		private SendOrPostCallback GetImItemsOperationCompleted;

		// Token: 0x04000147 RID: 327
		private SendOrPostCallback RemoveContactFromImListOperationCompleted;

		// Token: 0x04000148 RID: 328
		private SendOrPostCallback RemoveDistributionGroupFromImListOperationCompleted;

		// Token: 0x04000149 RID: 329
		private SendOrPostCallback RemoveImGroupOperationCompleted;

		// Token: 0x0400014A RID: 330
		private SendOrPostCallback SetImGroupOperationCompleted;

		// Token: 0x0400014B RID: 331
		private SendOrPostCallback SetImListMigrationCompletedOperationCompleted;

		// Token: 0x0400014C RID: 332
		private SendOrPostCallback GetUserRetentionPolicyTagsOperationCompleted;

		// Token: 0x0400014D RID: 333
		private SendOrPostCallback InstallAppOperationCompleted;

		// Token: 0x0400014E RID: 334
		private SendOrPostCallback UninstallAppOperationCompleted;

		// Token: 0x0400014F RID: 335
		private SendOrPostCallback DisableAppOperationCompleted;

		// Token: 0x04000150 RID: 336
		private SendOrPostCallback GetAppMarketplaceUrlOperationCompleted;

		// Token: 0x04000151 RID: 337
		private SendOrPostCallback GetUserPhotoOperationCompleted;
	}
}
