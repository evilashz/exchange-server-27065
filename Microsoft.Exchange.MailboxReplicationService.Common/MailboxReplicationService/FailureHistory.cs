using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200010A RID: 266
	public sealed class FailureHistory : XMLSerializableBase
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00012A8C File Offset: 0x00010C8C
		public FailureHistory()
		{
			this.Failures = new List<FailureHistory.MiniFailureRec>();
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00012A9F File Offset: 0x00010C9F
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00012AA7 File Offset: 0x00010CA7
		[XmlElement(ElementName = "F")]
		public List<FailureHistory.MiniFailureRec> Failures { get; set; }

		// Token: 0x0600095C RID: 2396 RVA: 0x00012AB0 File Offset: 0x00010CB0
		public void Add(Exception ex, bool isFatal, int maxHistoryLength)
		{
			if (ex == null || maxHistoryLength <= 0)
			{
				return;
			}
			while (this.Failures.Count >= maxHistoryLength)
			{
				this.Failures.RemoveAt(0);
			}
			this.Failures.Add(new FailureHistory.MiniFailureRec(ex, isFatal));
		}

		// Token: 0x0200010B RID: 267
		public sealed class MiniFailureRec : XMLSerializableBase
		{
			// Token: 0x0600095D RID: 2397 RVA: 0x00012AE6 File Offset: 0x00010CE6
			public MiniFailureRec()
			{
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x00012AF0 File Offset: 0x00010CF0
			internal MiniFailureRec(Exception ex, bool isFatal)
			{
				this.Timestamp = DateTime.UtcNow;
				this.FailureType = CommonUtils.GetFailureType(ex);
				this.FailureSide = CommonUtils.GetExceptionSide(ex);
				this.ExceptionTypes = CommonUtils.ClassifyException(ex);
				this.IsFatal = isFatal;
				this.ExceptionCallStackHash = CommonUtils.ComputeCallStackHash(ex, 5);
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x0600095F RID: 2399 RVA: 0x00012B46 File Offset: 0x00010D46
			// (set) Token: 0x06000960 RID: 2400 RVA: 0x00012B4E File Offset: 0x00010D4E
			[XmlAttribute(AttributeName = "Time")]
			public DateTime Timestamp { get; set; }

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x00012B57 File Offset: 0x00010D57
			// (set) Token: 0x06000962 RID: 2402 RVA: 0x00012B5F File Offset: 0x00010D5F
			[XmlAttribute(AttributeName = "Type")]
			public string FailureType { get; set; }

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000963 RID: 2403 RVA: 0x00012B68 File Offset: 0x00010D68
			// (set) Token: 0x06000964 RID: 2404 RVA: 0x00012B70 File Offset: 0x00010D70
			[XmlIgnore]
			public ExceptionSide? FailureSide { get; private set; }

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000965 RID: 2405 RVA: 0x00012B7C File Offset: 0x00010D7C
			// (set) Token: 0x06000966 RID: 2406 RVA: 0x00012BA4 File Offset: 0x00010DA4
			[XmlAttribute(AttributeName = "Side")]
			public int FailureSideInt
			{
				get
				{
					ExceptionSide? failureSide = this.FailureSide;
					if (failureSide == null)
					{
						return 0;
					}
					return (int)failureSide.GetValueOrDefault();
				}
				set
				{
					if (value == 0)
					{
						this.FailureSide = null;
						return;
					}
					this.FailureSide = new ExceptionSide?((ExceptionSide)value);
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000967 RID: 2407 RVA: 0x00012BD0 File Offset: 0x00010DD0
			// (set) Token: 0x06000968 RID: 2408 RVA: 0x00012BD8 File Offset: 0x00010DD8
			[XmlAttribute(AttributeName = "Fatal")]
			public bool IsFatal { get; set; }

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000969 RID: 2409 RVA: 0x00012BE1 File Offset: 0x00010DE1
			// (set) Token: 0x0600096A RID: 2410 RVA: 0x00012BE9 File Offset: 0x00010DE9
			[XmlIgnore]
			public WellKnownException[] ExceptionTypes { get; private set; }

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x0600096B RID: 2411 RVA: 0x00012BF4 File Offset: 0x00010DF4
			// (set) Token: 0x0600096C RID: 2412 RVA: 0x00012C44 File Offset: 0x00010E44
			[XmlAttribute(AttributeName = "ETypes")]
			public int[] ExceptionTypesInt
			{
				get
				{
					if (this.ExceptionTypes == null || this.ExceptionTypes.Length == 0)
					{
						return null;
					}
					int[] array = new int[this.ExceptionTypes.Length];
					for (int i = 0; i < this.ExceptionTypes.Length; i++)
					{
						array[i] = (int)this.ExceptionTypes[i];
					}
					return array;
				}
				set
				{
					if (value == null || value.Length == 0)
					{
						this.ExceptionTypes = null;
						return;
					}
					this.ExceptionTypes = new WellKnownException[value.Length];
					for (int i = 0; i < value.Length; i++)
					{
						this.ExceptionTypes[i] = (WellKnownException)value[i];
					}
				}
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x0600096D RID: 2413 RVA: 0x00012C88 File Offset: 0x00010E88
			// (set) Token: 0x0600096E RID: 2414 RVA: 0x00012C90 File Offset: 0x00010E90
			[XmlAttribute(AttributeName = "ExStackHash")]
			public string ExceptionCallStackHash { get; set; }
		}
	}
}
