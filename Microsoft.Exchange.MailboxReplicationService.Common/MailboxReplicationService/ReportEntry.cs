using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000192 RID: 402
	[XmlType(TypeName = "MoveReportEntry")]
	[Serializable]
	public sealed class ReportEntry : XMLSerializableBase, ILocalizedString
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x000224F5 File Offset: 0x000206F5
		public ReportEntry()
		{
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000224FD File Offset: 0x000206FD
		public ReportEntry(LocalizedString message) : this(message, ReportEntryType.Informational)
		{
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00022507 File Offset: 0x00020707
		public ReportEntry(LocalizedString message, ReportEntryType type) : this(message, type, null, ReportEntryFlags.None)
		{
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00022514 File Offset: 0x00020714
		public ReportEntry(LocalizedString message, ReportEntryType type, Exception failure, ReportEntryFlags flags)
		{
			this.Message = message;
			this.CreationTime = (DateTime)ExDateTime.UtcNow;
			this.ServerName = CommonUtils.LocalShortComputerName;
			this.Type = type;
			this.Flags = flags;
			if (failure != null)
			{
				this.Failure = FailureRec.Create(failure);
				this.Flags |= ReportEntryFlags.Failure;
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00022575 File Offset: 0x00020775
		private ReportEntry(DateTime creationTime)
		{
			this.CreationTime = creationTime;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00022584 File Offset: 0x00020784
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0002258C File Offset: 0x0002078C
		[XmlElement(ElementName = "CreationTime")]
		public DateTime CreationTime { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00022595 File Offset: 0x00020795
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0002259D File Offset: 0x0002079D
		[XmlElement(ElementName = "ServerName")]
		public string ServerName { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000225A6 File Offset: 0x000207A6
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x000225AE File Offset: 0x000207AE
		[XmlIgnore]
		public ReportEntryType Type { get; internal set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x000225B7 File Offset: 0x000207B7
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x000225BF File Offset: 0x000207BF
		[XmlElement(ElementName = "Type")]
		public int TypeInt
		{
			get
			{
				return (int)this.Type;
			}
			set
			{
				this.Type = (ReportEntryType)value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000225C8 File Offset: 0x000207C8
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000225D0 File Offset: 0x000207D0
		[XmlIgnore]
		public ReportEntryFlags Flags { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000225D9 File Offset: 0x000207D9
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000225E1 File Offset: 0x000207E1
		[XmlElement(ElementName = "Flags")]
		public int FlagsInt
		{
			get
			{
				return (int)this.Flags;
			}
			set
			{
				this.Flags = (ReportEntryFlags)value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000225EA File Offset: 0x000207EA
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000225F2 File Offset: 0x000207F2
		[XmlIgnore]
		public LocalizedString Message { get; private set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000225FB File Offset: 0x000207FB
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00022608 File Offset: 0x00020808
		[XmlElement(ElementName = "Message")]
		public byte[] MessageData
		{
			get
			{
				return CommonUtils.ByteSerialize(this.Message);
			}
			set
			{
				this.Message = CommonUtils.ByteDeserialize(value);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00022616 File Offset: 0x00020816
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0002261E File Offset: 0x0002081E
		[XmlElement(ElementName = "Failure")]
		public FailureRec Failure { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00022627 File Offset: 0x00020827
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0002262F File Offset: 0x0002082F
		[XmlElement(ElementName = "BadItem")]
		public BadMessageRec BadItem { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00022638 File Offset: 0x00020838
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00022640 File Offset: 0x00020840
		[XmlElement(ElementName = "ConfigObject")]
		public ConfigurableObjectXML ConfigObject { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00022649 File Offset: 0x00020849
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00022651 File Offset: 0x00020851
		[XmlElement(ElementName = "MailboxSize")]
		public MailboxSizeRec MailboxSize { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0002265A File Offset: 0x0002085A
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00022662 File Offset: 0x00020862
		[XmlElement(ElementName = "SessionStatistics")]
		public SessionStatistics SessionStatistics { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002266B File Offset: 0x0002086B
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00022673 File Offset: 0x00020873
		[XmlElement(ElementName = "ArchiveSessionStatistics")]
		public SessionStatistics ArchiveSessionStatistics { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0002267C File Offset: 0x0002087C
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x00022684 File Offset: 0x00020884
		[XmlArrayItem("FolderSize")]
		[XmlArray("MailboxVerificationResults")]
		public List<FolderSizeRec> MailboxVerificationResults { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002268D File Offset: 0x0002088D
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00022695 File Offset: 0x00020895
		[XmlElement(ElementName = "DebugData")]
		public string DebugData { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0002269E File Offset: 0x0002089E
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x000226A6 File Offset: 0x000208A6
		[XmlElement(ElementName = "Connectivity")]
		public ConnectivityRec Connectivity { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000226AF File Offset: 0x000208AF
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x000226B7 File Offset: 0x000208B7
		[XmlElement(ElementName = "SourceThrottles")]
		public ThrottleDurations SourceThrottleDurations { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000226C0 File Offset: 0x000208C0
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x000226C8 File Offset: 0x000208C8
		[XmlElement(ElementName = "TargetThrottles")]
		public ThrottleDurations TargetThrottleDurations { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000226D4 File Offset: 0x000208D4
		LocalizedString ILocalizedString.LocalizedString
		{
			get
			{
				LocalizedString message = (this.Type == ReportEntryType.Debug) ? new LocalizedString(this.DebugData) : this.Message;
				return MrsStrings.MoveReportEntryMessage(this.CreationTime.ToLocalTime().ToString(), this.ServerName, message);
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00022728 File Offset: 0x00020928
		public override string ToString()
		{
			return ((ILocalizedString)this).LocalizedString.ToString();
		}

		// Token: 0x0400087C RID: 2172
		internal static ReportEntry MaxEntry = new ReportEntry(DateTime.MaxValue);
	}
}
