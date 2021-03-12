using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200010D RID: 269
	internal class MailItemStorage : DataRow, IMailItemStorage
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00028A88 File Offset: 0x00026C88
		private static void TransactionCallback(IAsyncResult asyncResult)
		{
			Tuple<Transaction, AsyncResult> tuple = (Tuple<Transaction, AsyncResult>)asyncResult.AsyncState;
			try
			{
				tuple.Item1.EndAsyncCommit(asyncResult);
			}
			catch (Exception exception)
			{
				tuple.Item2.Exception = exception;
			}
			tuple.Item2.IsCompleted = true;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00028ADC File Offset: 0x00026CDC
		private void InitComponents()
		{
			this.blobCollection = new BlobCollection(this.Table.Schemas[21], this);
			this.componentMimeCache = new MimeCache(this);
			this.componentXexch50 = new XExch50Blob(this, this.blobCollection, 5);
			this.componentFastIndexBlob = new LazyBytes(this, this.blobCollection, 6);
			this.componentExtendedProperties = new ExtendedPropertyDictionary(this, this.blobCollection, 3);
			this.componentInternalProperties = new ExtendedPropertyDictionary(this, this.blobCollection, 4);
			base.AddComponent(this.componentExtendedProperties);
			base.AddComponent(this.componentInternalProperties);
			base.AddComponent(this.componentXexch50);
			base.AddComponent(this.componentFastIndexBlob);
			base.AddFirstComponent(this.componentMimeCache);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00028B9C File Offset: 0x00026D9C
		private void LoadDefaults()
		{
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
			this.IsDiscardPending = false;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00028C65 File Offset: 0x00026E65
		public MailItemStorage(DataTable dataTable, bool loadDefaults) : base(dataTable)
		{
			this.InitComponents();
			if (loadDefaults)
			{
				this.LoadDefaults();
			}
			base.PerfCounters.NewMailItem.Increment();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00028C90 File Offset: 0x00026E90
		public MailItemStorage(DataTableCursor cursor) : base(cursor.Table)
		{
			this.InitComponents();
			base.LoadFromCurrentRow(cursor);
			if (this.IsActive)
			{
				this.Table.IncrementActiveMessageCount();
			}
			if (this.IsPending)
			{
				this.Table.IncrementPendingMessageCount();
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00028CDE File Offset: 0x00026EDE
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x00028CF9 File Offset: 0x00026EF9
		public new string PerfCounterAttribution
		{
			get
			{
				if (this.perfCounters != null)
				{
					return this.perfCounters.Name;
				}
				return string.Empty;
			}
			set
			{
				base.PerfCounterAttribution = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00028D02 File Offset: 0x00026F02
		public long MsgId
		{
			get
			{
				return this.Table.Generation.CombineIds(this.MessageRowId);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00028D1A File Offset: 0x00026F1A
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00028D32 File Offset: 0x00026F32
		public int MessageRowId
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[0]).Value;
			}
			protected set
			{
				((ColumnCache<int>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00028D4B File Offset: 0x00026F4B
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x00028D64 File Offset: 0x00026F64
		public string HeloDomain
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[14]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[14]).Value = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00028D8A File Offset: 0x00026F8A
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00028DA2 File Offset: 0x00026FA2
		public DateTime DateReceived
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[2]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<DateTime>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00028DC7 File Offset: 0x00026FC7
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x00028DE0 File Offset: 0x00026FE0
		public string Auth
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[16]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[16]).Value = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00028E06 File Offset: 0x00027006
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x00028E1F File Offset: 0x0002701F
		public string EnvId
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[12]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[12]).Value = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00028E45 File Offset: 0x00027045
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00028E5E File Offset: 0x0002705E
		public string ReceiveConnectorName
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[13]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[13]).Value = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00028E84 File Offset: 0x00027084
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00028E9C File Offset: 0x0002709C
		public int PoisonCount
		{
			get
			{
				return (int)((ColumnCache<byte>)base.Columns[6]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<byte>)base.Columns[6]).Value = (byte)value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00028EC2 File Offset: 0x000270C2
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00028EE0 File Offset: 0x000270E0
		public IPAddress SourceIPAddress
		{
			get
			{
				return ((ColumnCache<IPvxAddress>)base.Columns[3]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				IPvxAddress value2 = new IPvxAddress(value);
				((ColumnCache<IPvxAddress>)base.Columns[3]).Value = value2;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00028F18 File Offset: 0x00027118
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00028F30 File Offset: 0x00027130
		public Guid ShadowMessageId
		{
			get
			{
				return ((ColumnCache<Guid>)base.Columns[8]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<Guid>)base.Columns[8]).Value = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00028F55 File Offset: 0x00027155
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00028F6E File Offset: 0x0002716E
		public string ShadowServerContext
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[10]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[10]).Value = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00028F94 File Offset: 0x00027194
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00028FAD File Offset: 0x000271AD
		public string ShadowServerDiscardId
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[9]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[9]).Value = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00028FD3 File Offset: 0x000271D3
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x00028FEC File Offset: 0x000271EC
		public string FromAddress
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[15]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				((ColumnCache<string>)base.Columns[15]).Value = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00029014 File Offset: 0x00027214
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00029050 File Offset: 0x00027250
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				ColumnCache<int> columnCache = (ColumnCache<int>)base.Columns[7];
				if (!columnCache.HasValue)
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds((double)columnCache.Value);
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				if (value == TimeSpan.Zero)
				{
					((ColumnCache<int>)base.Columns[7]).HasValue = false;
					return;
				}
				double totalSeconds = value.TotalSeconds;
				if (totalSeconds > 2147483647.0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "ExtensionToExpiryDuration.TotalSeconds = '{0}' which is greater than Int32.MaxValue", new object[]
					{
						totalSeconds
					}));
				}
				((ColumnCache<int>)base.Columns[7]).Value = (int)totalSeconds;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000290E0 File Offset: 0x000272E0
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x000290E9 File Offset: 0x000272E9
		public bool IsDiscardPending
		{
			get
			{
				return this.GetPendingReasonValue(MailItemStorage.PendingReasons.DiscardPending);
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.SetPendingReasonValue(value, MailItemStorage.PendingReasons.DiscardPending);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000290F9 File Offset: 0x000272F9
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00029102 File Offset: 0x00027302
		public bool IsActive
		{
			get
			{
				return this.GetPendingReasonValue(MailItemStorage.PendingReasons.Active);
			}
			set
			{
				if (value == this.IsActive)
				{
					return;
				}
				this.ThrowIfDeleted();
				this.SetPendingReasonValue(value, MailItemStorage.PendingReasons.Active);
				if (value)
				{
					this.Table.IncrementActiveMessageCount();
					return;
				}
				this.Table.DecrementActiveMessageCount();
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00029138 File Offset: 0x00027338
		public bool IsPending
		{
			get
			{
				ColumnCache<byte> columnCache = (ColumnCache<byte>)base.Columns[22];
				return columnCache.HasValue && (columnCache.Value & 15) != 0;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00029174 File Offset: 0x00027374
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x000291A8 File Offset: 0x000273A8
		public DeliveryPriority BootloadingPriority
		{
			get
			{
				ColumnCache<byte> columnCache = (ColumnCache<byte>)base.Columns[22];
				if (!columnCache.HasValue)
				{
					return DeliveryPriority.Normal;
				}
				return (DeliveryPriority)(columnCache.Value >> 4);
			}
			set
			{
				this.ThrowIfDeleted();
				ColumnCache<byte> columnCache = (ColumnCache<byte>)base.Columns[22];
				if (columnCache.HasValue)
				{
					columnCache.Value = (byte)((int)(columnCache.Value & 15) | (int)((byte)value) << 4);
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000291EB File Offset: 0x000273EB
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x00029204 File Offset: 0x00027404
		public string Oorg
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[11]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<string>)base.Columns[11]).Value = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00029224 File Offset: 0x00027424
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0002923D File Offset: 0x0002743D
		public string MimeFrom
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[18]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<string>)base.Columns[18]).Value = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002925D File Offset: 0x0002745D
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x00029275 File Offset: 0x00027475
		public BodyType BodyType
		{
			get
			{
				return (BodyType)((ColumnCache<byte>)base.Columns[4]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<byte>)base.Columns[4]).Value = (byte)value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00029295 File Offset: 0x00027495
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x000292AE File Offset: 0x000274AE
		public string Subject
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[19]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<string>)base.Columns[19]).Value = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000292CE File Offset: 0x000274CE
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x000292E7 File Offset: 0x000274E7
		public string InternetMessageId
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[20]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<string>)base.Columns[20]).Value = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00029307 File Offset: 0x00027507
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x00029320 File Offset: 0x00027520
		public string MimeSenderAddress
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[17]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<string>)base.Columns[17]).Value = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00029340 File Offset: 0x00027540
		public bool IsInAsyncCommit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00029344 File Offset: 0x00027544
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00029374 File Offset: 0x00027574
		protected bool UnderConstruction
		{
			get
			{
				int value = ((ColumnCache<int>)base.Columns[1]).Value;
				return (value & 64) != 0;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				if (value)
				{
					num |= 64;
				}
				else
				{
					num &= -65;
				}
				((ColumnCache<int>)base.Columns[1]).Value = num;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x000293CC File Offset: 0x000275CC
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x000293F4 File Offset: 0x000275F4
		public DsnFormat DsnFormat
		{
			get
			{
				int value = ((ColumnCache<int>)base.Columns[1]).Value;
				return (DsnFormat)(value & 15);
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				num = ((-16 & num) | (int)((DsnFormat)15 & value));
				((ColumnCache<int>)base.Columns[1]).Value = num;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00029448 File Offset: 0x00027648
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0002947C File Offset: 0x0002767C
		public int Scl
		{
			get
			{
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				num = ((num & 3840) >> 8 & 15);
				return num - 2;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				value += 2;
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				num = ((-3841 & num) | (3840 & value << 8));
				((ColumnCache<int>)base.Columns[1]).Value = num;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000294D4 File Offset: 0x000276D4
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0002950C File Offset: 0x0002770C
		public MailDirectionality Directionality
		{
			get
			{
				int value = ((ColumnCache<int>)base.Columns[1]).Value;
				return (MailDirectionality)Convert.ToByte((value & 28672) >> 12 & 7);
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				num = ((-28673 & num) | (28672 & Convert.ToInt32(value) << 12));
				((ColumnCache<int>)base.Columns[1]).Value = num;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00029570 File Offset: 0x00027770
		public IExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.componentExtendedProperties;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00029578 File Offset: 0x00027778
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0002958F File Offset: 0x0002778F
		public string PrioritizationReason
		{
			get
			{
				return this.componentInternalProperties.GetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.PrioritizationReason", string.Empty);
			}
			set
			{
				this.componentInternalProperties.SetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.PrioritizationReason", value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000295A4 File Offset: 0x000277A4
		public bool IsJournalReport
		{
			get
			{
				string value;
				this.componentInternalProperties.TryGetValue<string>("Microsoft.Exchange.ContentIdentifier", out value);
				if (!string.IsNullOrEmpty(value))
				{
					return "EXJournalData".Equals(value, StringComparison.OrdinalIgnoreCase);
				}
				Header header = this.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Journal-Report");
				if (header != null)
				{
					if (!this.IsReadOnly)
					{
						this.componentInternalProperties.SetValue<string>("Microsoft.Exchange.ContentIdentifier", "EXJournalData");
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00029614 File Offset: 0x00027814
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0002963D File Offset: 0x0002783D
		public List<string> MoveToHosts
		{
			get
			{
				ReadOnlyCollection<string> collection;
				if (this.componentInternalProperties.TryGetListValue<string>("Microsoft.Exchange.Transport.DirectoryData.RedirectHosts", out collection))
				{
					return new List<string>(collection);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.componentInternalProperties.Remove("Microsoft.Exchange.Transport.DirectoryData.RedirectHosts");
					return;
				}
				this.componentInternalProperties.SetValue<List<string>>("Microsoft.Exchange.Transport.DirectoryData.RedirectHosts", value);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00029665 File Offset: 0x00027865
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0002966D File Offset: 0x0002786D
		public bool RetryDeliveryIfRejected
		{
			get
			{
				return this.retryDeliveryIfRejected;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.retryDeliveryIfRejected = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002967C File Offset: 0x0002787C
		public MultiValueHeader TransportPropertiesHeader
		{
			get
			{
				this.ThrowIfReadOnly();
				MultiValueHeader result;
				if ((result = this.transportPropertiesHeader) == null)
				{
					result = (this.transportPropertiesHeader = new MultiValueHeader(this, "X-MS-Exchange-Organization-Transport-Properties"));
				}
				return result;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000296AD File Offset: 0x000278AD
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x000296B8 File Offset: 0x000278B8
		public DeliveryPriority? Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				if (this.priority == value)
				{
					return;
				}
				this.priority = value;
				this.TransportPropertiesHeader.SetStringValue("DeliveryPriority", value.ToString());
				if (value == DeliveryPriority.Normal)
				{
					this.componentInternalProperties.Remove("Microsoft.Exchange.Transport.TransportMailItem.PrioritizationReason");
				}
				this.BootloadingPriority = (value ?? DeliveryPriority.Normal);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00029764 File Offset: 0x00027964
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002978C File Offset: 0x0002798C
		public Guid NetworkMessageId
		{
			get
			{
				Guid result;
				if (this.componentInternalProperties.TryGetValue<Guid>("Microsoft.Exchange.Transport.MailRecipient.NetworkMessageId", out result))
				{
					return result;
				}
				return Guid.Empty;
			}
			set
			{
				this.componentInternalProperties.SetValue<Guid>("Microsoft.Exchange.Transport.MailRecipient.NetworkMessageId", value);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000297A0 File Offset: 0x000279A0
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000297C8 File Offset: 0x000279C8
		public Guid SystemProbeId
		{
			get
			{
				Guid result;
				if (this.componentInternalProperties.TryGetValue<Guid>("Microsoft.Exchange.Transport.TransportMailItem.SystemProbeId", out result))
				{
					return result;
				}
				return Guid.Empty;
			}
			set
			{
				this.componentInternalProperties.SetValue<Guid>("Microsoft.Exchange.Transport.TransportMailItem.SystemProbeId", value);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000297DB File Offset: 0x000279DB
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x000297EE File Offset: 0x000279EE
		public RiskLevel RiskLevel
		{
			get
			{
				return (RiskLevel)this.componentInternalProperties.GetValue<int>("Microsoft.Exchange.Transport.TransportMailItem.RiskLevel", 0);
			}
			set
			{
				this.componentInternalProperties.SetValue<int>("Microsoft.Exchange.Transport.TransportMailItem.RiskLevel", (int)value);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00029804 File Offset: 0x00027A04
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x00029828 File Offset: 0x00027A28
		public string ExoAccountForest
		{
			get
			{
				string result;
				if (this.componentInternalProperties.TryGetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.ExoAccountForest", out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				this.componentInternalProperties.SetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.ExoAccountForest", value);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002983C File Offset: 0x00027A3C
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00029860 File Offset: 0x00027A60
		public string ExoTenantContainer
		{
			get
			{
				string result;
				if (this.componentInternalProperties.TryGetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.ExoTenantContainer", out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				this.componentInternalProperties.SetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.ExoTenantContainer", value);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00029874 File Offset: 0x00027A74
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002989C File Offset: 0x00027A9C
		public Guid ExternalOrganizationId
		{
			get
			{
				Guid result;
				if (this.componentInternalProperties.TryGetValue<Guid>("Microsoft.Exchange.Transport.TransportMailItem.ExternalOrganizationId", out result))
				{
					return result;
				}
				return Guid.Empty;
			}
			set
			{
				this.componentInternalProperties.SetValue<Guid>("Microsoft.Exchange.Transport.TransportMailItem.ExternalOrganizationId", value);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000298B0 File Offset: 0x00027AB0
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x000298D4 File Offset: 0x00027AD4
		public string AttributedFromAddress
		{
			get
			{
				string result;
				if (this.componentInternalProperties.TryGetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.AttributedFromAddress", out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				this.componentInternalProperties.SetValue<string>("Microsoft.Exchange.Transport.TransportMailItem.AttributedFromAddress", value);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000298E7 File Offset: 0x00027AE7
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x000298F0 File Offset: 0x00027AF0
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.componentMimeCache.SetReadOnly(value);
				this.componentExtendedProperties.IsReadOnly = value;
				this.componentInternalProperties.IsReadOnly = value;
				this.componentXexch50.IsReadOnly = value;
				this.componentFastIndexBlob.IsReadOnly = value;
				this.isReadOnly = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00029940 File Offset: 0x00027B40
		public new MessageTable Table
		{
			get
			{
				return (MessageTable)base.Table;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002994D File Offset: 0x00027B4D
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0002995A File Offset: 0x00027B5A
		public MimeDocument MimeDocument
		{
			get
			{
				return this.componentMimeCache.MimeDocument;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.componentMimeCache.SetMimeDocument(value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00029974 File Offset: 0x00027B74
		public EmailMessage Message
		{
			get
			{
				return this.componentMimeCache.Message;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00029981 File Offset: 0x00027B81
		public MimePart RootPart
		{
			get
			{
				return this.componentMimeCache.RootPart;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0002998E File Offset: 0x00027B8E
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x000299A6 File Offset: 0x00027BA6
		public long MimeSize
		{
			get
			{
				return ((ColumnCache<long>)base.Columns[5]).Value;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				((ColumnCache<long>)base.Columns[5]).Value = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000299C8 File Offset: 0x00027BC8
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000299FC File Offset: 0x00027BFC
		public bool MimeNotSequential
		{
			get
			{
				int value = ((ColumnCache<int>)base.Columns[1]).Value;
				return (value & 128) != 0;
			}
			set
			{
				this.ThrowIfDeleted();
				int num = ((ColumnCache<int>)base.Columns[1]).Value;
				if (value)
				{
					num |= 128;
				}
				else
				{
					num &= -129;
				}
				((ColumnCache<int>)base.Columns[1]).Value = num;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00029A52 File Offset: 0x00027C52
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00029A5F File Offset: 0x00027C5F
		public bool FallbackToRawOverride
		{
			get
			{
				return this.componentMimeCache.FallbackToRawOverride;
			}
			set
			{
				this.componentMimeCache.FallbackToRawOverride = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00029A6D File Offset: 0x00027C6D
		public bool MimeWriteStreamOpen
		{
			get
			{
				return this.componentMimeCache.MimeWriteStreamOpen;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00029A7A File Offset: 0x00027C7A
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00029A87 File Offset: 0x00027C87
		public Encoding MimeDefaultEncoding
		{
			get
			{
				return this.componentMimeCache.DefaultEncoding;
			}
			set
			{
				this.componentMimeCache.DefaultEncoding = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00029A95 File Offset: 0x00027C95
		public XExch50Blob XExch50Blob
		{
			get
			{
				return this.componentXexch50;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00029A9D File Offset: 0x00027C9D
		public LazyBytes FastIndexBlob
		{
			get
			{
				return this.componentFastIndexBlob;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00029AA5 File Offset: 0x00027CA5
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00029AAD File Offset: 0x00027CAD
		public string ProbeName { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00029AB6 File Offset: 0x00027CB6
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00029ABE File Offset: 0x00027CBE
		public bool PersistProbeTrace { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00029AC7 File Offset: 0x00027CC7
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00029ACF File Offset: 0x00027CCF
		public IDataExternalComponent Recipients
		{
			get
			{
				return this.componentRecipients;
			}
			set
			{
				if (value != this.componentRecipients)
				{
					if (this.componentRecipients != null)
					{
						base.ReplaceComponent(this.componentRecipients, value);
					}
					else
					{
						base.AddComponent(value);
					}
					this.componentRecipients = value;
				}
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00029B00 File Offset: 0x00027D00
		public static MailItemStorage LoadFromRow(DataTableCursor cursor)
		{
			MailItemStorage mailItemStorage = new MailItemStorage(cursor);
			mailItemStorage.PerfCounters.LoadMailItem.Increment();
			return mailItemStorage;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00029B26 File Offset: 0x00027D26
		public override void MinimizeMemory()
		{
			base.MinimizeMemory();
			base.PerfCounters.DehydrateMailItem.Increment();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00029B3F File Offset: 0x00027D3F
		public void ReleaseFromActive()
		{
			this.IsActive = false;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00029B48 File Offset: 0x00027D48
		public override void MarkToDelete()
		{
			base.MarkToDelete();
			this.Table.DecrementMessageCount();
			if (this.IsActive)
			{
				this.Table.DecrementActiveMessageCount();
			}
			if (this.IsPending)
			{
				this.Table.DecrementPendingMessageCount();
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00029B84 File Offset: 0x00027D84
		public Stream OpenMimeReadStream(bool downConvert)
		{
			return this.componentMimeCache.OpenMimeReadStream(downConvert);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00029B92 File Offset: 0x00027D92
		public Stream OpenMimeWriteStream(MimeLimits mimeLimits, bool expectBinaryContent)
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			return this.componentMimeCache.OpenMimeWriteStream(mimeLimits, expectBinaryContent);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00029BAD File Offset: 0x00027DAD
		public long GetCurrrentMimeSize()
		{
			this.ThrowIfInAsyncCommit();
			return this.componentMimeCache.MimeStreamSize;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00029BC0 File Offset: 0x00027DC0
		public long RefreshMimeSize()
		{
			this.ThrowIfDeletedOrReadOnly();
			return this.GetCurrrentMimeSize();
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00029BCE File Offset: 0x00027DCE
		public void RestoreLastSavedMime()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			this.componentMimeCache.RestoreLastSavedMime();
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00029BE7 File Offset: 0x00027DE7
		public void UpdateCachedHeaders()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			this.componentMimeCache.PromoteHeaders();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00029C00 File Offset: 0x00027E00
		public void ResetMimeParserEohCallback()
		{
			this.componentMimeCache.ResetMimeParserEohCallback();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00029C10 File Offset: 0x00027E10
		public new void Commit(TransactionCommitMode commitMode)
		{
			this.audit.Drop((commitMode == TransactionCommitMode.Lazy) ? Breadcrumb.CommitLazy : Breadcrumb.CommitNow);
			base.Commit(commitMode);
			((commitMode == TransactionCommitMode.Immediate) ? base.PerfCounters.CommitImmediateMailItem : base.PerfCounters.CommitLazyMailItem).Increment();
			if (this.IsDeleted)
			{
				((commitMode == TransactionCommitMode.Immediate) ? base.PerfCounters.Delete : base.PerfCounters.DeleteLazyMailItem).Increment();
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00029C8A File Offset: 0x00027E8A
		public new void Materialize(Transaction transaction)
		{
			base.Materialize(transaction);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00029C93 File Offset: 0x00027E93
		protected override void MaterializeToCursor(Transaction transaction, DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			if (this.MessageRowId == 0)
			{
				this.AssignMessageId(this.Table.GetNextMessageRowId());
			}
			base.MaterializeToCursor(transaction, cursor, checkpointCallback);
			base.PerfCounters.MaterializeMailItem.Increment();
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00029CC8 File Offset: 0x00027EC8
		public IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState)
		{
			AsyncResult asyncResult = new AsyncResult(asyncCallback, asyncState);
			if (!base.PendingDatabaseUpdates)
			{
				asyncResult.CompletedSynchronously = true;
				asyncResult.IsCompleted = true;
				return asyncResult;
			}
			base.PerfCounters.BeginCommitMailItem.Increment();
			try
			{
				using (DataConnection dataConnection = this.Table.DataSource.DemandNewConnection())
				{
					using (Transaction transaction = dataConnection.BeginTransaction())
					{
						this.Materialize(transaction);
						transaction.BeginAsyncCommit(MailItemStorage.DefaultAsyncCommitTimeout, Tuple.Create<Transaction, AsyncResult>(transaction, asyncResult), new AsyncCallback(MailItemStorage.TransactionCallback));
					}
				}
			}
			catch (Exception exception)
			{
				asyncResult.Exception = exception;
				asyncResult.CompletedSynchronously = true;
				asyncResult.IsCompleted = true;
			}
			return asyncResult;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00029DA0 File Offset: 0x00027FA0
		public bool EndCommit(IAsyncResult ar, out Exception exception)
		{
			AsyncResult asyncResult = (AsyncResult)ar;
			exception = null;
			try
			{
				asyncResult.End();
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return null == exception;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00029DDC File Offset: 0x00027FDC
		public void CloneTo(MailItemStorage newMailItemStorage)
		{
			newMailItemStorage.Columns.CloneFrom(base.Columns);
			newMailItemStorage.componentExtendedProperties.CloneFrom(this.componentExtendedProperties);
			newMailItemStorage.componentInternalProperties.CloneFrom(this.componentInternalProperties);
			newMailItemStorage.ProbeName = this.ProbeName;
			newMailItemStorage.PersistProbeTrace = this.PersistProbeTrace;
			if (this.Table == newMailItemStorage.Table)
			{
				newMailItemStorage.SetCloneOrMoveSource(this, true);
				((IDataObjectComponent)newMailItemStorage.componentXexch50).CloneFrom(this.componentXexch50);
				((IDataObjectComponent)newMailItemStorage.componentFastIndexBlob).CloneFrom(this.componentFastIndexBlob);
				((IDataObjectComponent)newMailItemStorage.componentMimeCache).CloneFrom(this.componentMimeCache);
			}
			else
			{
				newMailItemStorage.Columns.MarkDirtyForReload();
				newMailItemStorage.componentExtendedProperties.Dirty = true;
				newMailItemStorage.componentInternalProperties.Dirty = true;
				newMailItemStorage.componentXexch50.Value = this.componentXexch50.Value;
				newMailItemStorage.componentFastIndexBlob.Value = this.componentFastIndexBlob.Value;
				using (Stream stream = newMailItemStorage.componentMimeCache.OpenMimeWriteStream(MimeLimits.Unlimited, true))
				{
					using (Stream stream2 = this.componentMimeCache.OpenMimeReadStream(false))
					{
						byte[] array = new byte[65536];
						int count;
						while ((count = stream2.Read(array, 0, array.Length)) > 0)
						{
							stream.Write(array, 0, count);
						}
						stream2.Close();
						stream.Close();
					}
				}
			}
			newMailItemStorage.MessageRowId = 0;
			newMailItemStorage.ShadowMessageId = Guid.NewGuid();
			if (newMailItemStorage.IsActive)
			{
				newMailItemStorage.Table.IncrementActiveMessageCount();
			}
			if (newMailItemStorage.IsPending)
			{
				newMailItemStorage.Table.IncrementPendingMessageCount();
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00029F90 File Offset: 0x00028190
		public IMailItemStorage CloneCommitted(Action<IMailItemStorage> cloneVisitor)
		{
			MailItemStorage mailItemStorage = new MailItemStorage(this.Table, false);
			IMailItemStorage result;
			using (Transaction transaction = this.Table.DataSource.BeginNewTransaction())
			{
				this.Materialize(transaction);
				this.CloneTo(mailItemStorage);
				cloneVisitor(mailItemStorage);
				mailItemStorage.Materialize(transaction);
				transaction.Commit();
				result = mailItemStorage;
			}
			return result;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00029FFC File Offset: 0x000281FC
		public void AtomicChange(Action<IMailItemStorage> changeAction)
		{
			using (Transaction transaction = this.Table.DataSource.BeginNewTransaction())
			{
				this.Materialize(transaction);
				changeAction(this);
				transaction.Commit();
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002A04C File Offset: 0x0002824C
		public IMailItemStorage CopyCommitted(Action<IMailItemStorage> copyVisitor)
		{
			base.Commit();
			DataGeneration generation = this.Table.Generation;
			MailItemStorage mailItemStorage = (MailItemStorage)generation.MessagingDatabase.NewMailItemStorage(false);
			IMailItemStorage result;
			using (Transaction transaction = mailItemStorage.Table.DataSource.BeginNewTransaction())
			{
				this.CloneTo(mailItemStorage);
				copyVisitor(mailItemStorage);
				mailItemStorage.Materialize(transaction);
				transaction.Commit();
				result = mailItemStorage;
			}
			return result;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002A0C8 File Offset: 0x000282C8
		IMailItemStorage IMailItemStorage.Clone()
		{
			DataGeneration generation = this.Table.Generation;
			MailItemStorage mailItemStorage = (MailItemStorage)generation.MessagingDatabase.NewMailItemStorage(false);
			this.CloneTo(mailItemStorage);
			return mailItemStorage;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002A0FB File Offset: 0x000282FB
		private void AssignMessageId(int newId)
		{
			this.MessageRowId = newId;
			if (this.Recipients != null)
			{
				this.Recipients.ParentPrimaryKeyChanged();
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002A117 File Offset: 0x00028317
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException("This operation cannot be performed in read-only mode. Mail item queued for delivery?");
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002A12C File Offset: 0x0002832C
		private void ThrowIfInAsyncCommit()
		{
			if (this.IsInAsyncCommit)
			{
				throw new InvalidOperationException("This operation cannot be performed when mail item is in Async Commit");
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002A141 File Offset: 0x00028341
		private void ThrowIfDeleted()
		{
			if (this.IsDeleted)
			{
				throw new InvalidOperationException("operations not allowed on a deleted mail item");
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002A156 File Offset: 0x00028356
		private void ThrowIfDeletedOrReadOnly()
		{
			this.ThrowIfDeleted();
			this.ThrowIfReadOnly();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002A194 File Offset: 0x00028394
		public void OpenMimeDBWriter(DataTableCursor cursor, bool update, Func<bool> checkpointCallback, out Stream mimeMap, out CreateFixedStream mimeCreateFixedStream)
		{
			mimeCreateFixedStream = (() => this.blobCollection.OpenWriter(1, cursor, update, true, checkpointCallback));
			mimeMap = this.blobCollection.OpenWriter(2, cursor, update, false, null);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002A20C File Offset: 0x0002840C
		public MimeDocument LoadMimeFromDB(DecodingOptions decodingOptions)
		{
			MimeDocument result;
			using (DataTableCursor cursor = this.Table.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					base.SeekCurrent(cursor);
					using (Stream stream = this.blobCollection.OpenReader(2, cursor, false))
					{
						result = MimeCacheMap.Load(stream, () => this.OpenMimeDBReader(cursor), decodingOptions);
					}
				}
			}
			return result;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002A2D4 File Offset: 0x000284D4
		public Stream OpenMimeDBReader(DataTableCursor cursor)
		{
			return this.blobCollection.OpenReader(1, cursor, true);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002A2E4 File Offset: 0x000284E4
		public Stream OpenMimeDBReader()
		{
			Stream result;
			using (DataTableCursor cursor = this.Table.GetCursor())
			{
				base.SeekCurrent(cursor);
				result = this.OpenMimeDBReader(cursor);
			}
			return result;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A32C File Offset: 0x0002852C
		public Stream OpenMimeMapReader(DataTableCursor cursor)
		{
			return this.blobCollection.OpenReader(2, cursor, false);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A33C File Offset: 0x0002853C
		private bool GetPendingReasonValue(MailItemStorage.PendingReasons reason)
		{
			ColumnCache<byte> columnCache = (ColumnCache<byte>)base.Columns[22];
			return columnCache.HasValue && ((MailItemStorage.PendingReasons)columnCache.Value & reason) == reason;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A374 File Offset: 0x00028574
		private void SetPendingReasonValue(bool set, MailItemStorage.PendingReasons reason)
		{
			ColumnCache<byte> columnCache = (ColumnCache<byte>)base.Columns[22];
			MailItemStorage.PendingReasons pendingReasons = (MailItemStorage.PendingReasons)(columnCache.HasValue ? (columnCache.Value & 15) : 0);
			if (set)
			{
				pendingReasons |= reason;
			}
			else
			{
				pendingReasons &= ~reason;
			}
			if (!columnCache.HasValue && pendingReasons != MailItemStorage.PendingReasons.None)
			{
				this.Table.IncrementPendingMessageCount();
			}
			if (columnCache.HasValue && pendingReasons == MailItemStorage.PendingReasons.None)
			{
				this.Table.DecrementPendingMessageCount();
			}
			columnCache.Value = (byte)(pendingReasons | ((MailItemStorage.PendingReasons)columnCache.Value & (MailItemStorage.PendingReasons)(-16)));
			columnCache.HasValue = (pendingReasons != MailItemStorage.PendingReasons.None);
		}

		// Token: 0x040004EF RID: 1263
		private const int DsnFormatMask = 15;

		// Token: 0x040004F0 RID: 1264
		private const int UnderConstructionMask = 64;

		// Token: 0x040004F1 RID: 1265
		private const int MimeNotSequentialMask = 128;

		// Token: 0x040004F2 RID: 1266
		private const int SclOffset = 8;

		// Token: 0x040004F3 RID: 1267
		private const int SclMask = 3840;

		// Token: 0x040004F4 RID: 1268
		private const int DirectionalityOffset = 12;

		// Token: 0x040004F5 RID: 1269
		private const int DirectionalityMask = 28672;

		// Token: 0x040004F6 RID: 1270
		private const int CloneMimeBlockSize = 65536;

		// Token: 0x040004F7 RID: 1271
		internal static TimeSpan DefaultAsyncCommitTimeout = TimeSpan.FromMilliseconds(250.0);

		// Token: 0x040004F8 RID: 1272
		private IDataExternalComponent componentRecipients;

		// Token: 0x040004F9 RID: 1273
		private MimeCache componentMimeCache;

		// Token: 0x040004FA RID: 1274
		private XExch50Blob componentXexch50;

		// Token: 0x040004FB RID: 1275
		private LazyBytes componentFastIndexBlob;

		// Token: 0x040004FC RID: 1276
		private bool isReadOnly;

		// Token: 0x040004FD RID: 1277
		private bool retryDeliveryIfRejected;

		// Token: 0x040004FE RID: 1278
		private MultiValueHeader transportPropertiesHeader;

		// Token: 0x040004FF RID: 1279
		private DeliveryPriority? priority;

		// Token: 0x04000500 RID: 1280
		private ExtendedPropertyDictionary componentExtendedProperties;

		// Token: 0x04000501 RID: 1281
		private ExtendedPropertyDictionary componentInternalProperties;

		// Token: 0x04000502 RID: 1282
		private BlobCollection blobCollection;

		// Token: 0x0200010E RID: 270
		private enum BlobCollectionKeys : byte
		{
			// Token: 0x04000506 RID: 1286
			MimeStream = 1,
			// Token: 0x04000507 RID: 1287
			MimeMap,
			// Token: 0x04000508 RID: 1288
			ExtendedProperties,
			// Token: 0x04000509 RID: 1289
			InternalProperties,
			// Token: 0x0400050A RID: 1290
			XExch50,
			// Token: 0x0400050B RID: 1291
			FastIndex
		}

		// Token: 0x0200010F RID: 271
		[Flags]
		internal enum PendingReasons
		{
			// Token: 0x0400050D RID: 1293
			None = 0,
			// Token: 0x0400050E RID: 1294
			DiscardPending = 1,
			// Token: 0x0400050F RID: 1295
			Active = 2,
			// Token: 0x04000510 RID: 1296
			All = 15
		}
	}
}
