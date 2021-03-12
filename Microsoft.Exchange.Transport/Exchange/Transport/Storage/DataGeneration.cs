using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000DD RID: 221
	internal abstract class DataGeneration
	{
		// Token: 0x060007DE RID: 2014
		protected abstract void Attach(Transaction transaction);

		// Token: 0x060007DF RID: 2015
		protected abstract void Detach();

		// Token: 0x060007E0 RID: 2016
		protected abstract GenerationCleanupMode CleanupInternal();

		// Token: 0x060007E1 RID: 2017
		protected abstract bool DropInternal();

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001F8E3 File Offset: 0x0001DAE3
		public bool IsAttached
		{
			get
			{
				return this.isAttached;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001F8EB File Offset: 0x0001DAEB
		public DataGenerationCategory Category
		{
			get
			{
				return (DataGenerationCategory)this.dataRow.Category;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		public DateTime EndTime
		{
			get
			{
				return this.dataRow.EndTime;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001F905 File Offset: 0x0001DB05
		public int GenerationId
		{
			get
			{
				return this.dataRow.GenerationId;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001F912 File Offset: 0x0001DB12
		public DateTime StartTime
		{
			get
			{
				return this.dataRow.StartTime;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001F91F File Offset: 0x0001DB1F
		public string Name
		{
			get
			{
				return this.dataRow.Name;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001F92C File Offset: 0x0001DB2C
		public TimeSpan Duration
		{
			get
			{
				return this.EndTime - this.StartTime;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001F93F File Offset: 0x0001DB3F
		public IMessagingDatabase MessagingDatabase
		{
			get
			{
				return ((DataGenerationTable)this.dataRow.Table).MessagingDatabase;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001F956 File Offset: 0x0001DB56
		public void Load(DataGenerationRow row)
		{
			if (this.dataRow != null)
			{
				throw new InvalidOperationException("This generation is already loaded.");
			}
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			this.dataRow = row;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001F980 File Offset: 0x0001DB80
		public void Attach()
		{
			if (this.isAttached)
			{
				return;
			}
			lock (this.attachLock)
			{
				if (!this.isAttached)
				{
					using (DataConnection dataConnection = this.dataRow.Table.DataSource.DemandNewConnection())
					{
						using (Transaction transaction = dataConnection.BeginTransaction())
						{
							this.Attach(transaction);
							transaction.Commit(TransactionCommitMode.Lazy);
						}
					}
					this.isAttached = true;
				}
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001FA30 File Offset: 0x0001DC30
		public void Load(DateTime startTime, DateTime endTime, DataGenerationCategory category, DataGenerationTable table)
		{
			if (this.dataRow != null)
			{
				throw new InvalidOperationException("This generation is already loaded.");
			}
			ExTraceGlobals.StorageTracer.TraceDebug<DateTime, DateTime, DataGenerationCategory>((long)this.GetHashCode(), "Creating data generation (startTime={0}, endTime={1}, category={2})", startTime, endTime, category);
			if (endTime < startTime)
			{
				throw new ArgumentOutOfRangeException("endTime", "Generation size can't be negative. Start and End might be switched.");
			}
			this.dataRow = new DataGenerationRow(table)
			{
				GenerationId = table.GetNextGenerationId(),
				StartTime = startTime,
				EndTime = endTime,
				Category = (int)category,
				Name = string.Format(CultureInfo.InvariantCulture, "{0}-{1:yyyyMMddHHmmss}-{2:yyyyMMddHHmmss}", new object[]
				{
					category,
					startTime,
					endTime
				})
			};
			using (DataConnection dataConnection = table.DataSource.DemandNewConnection())
			{
				using (Transaction transaction = dataConnection.BeginTransaction())
				{
					this.dataRow.Commit(transaction);
					this.Attach(transaction);
					transaction.Commit();
					this.isAttached = true;
				}
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public void Unload()
		{
			if (this.isAttached)
			{
				lock (this.attachLock)
				{
					if (this.isAttached)
					{
						this.Detach();
						this.isAttached = false;
					}
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		public bool Contains(DateTime timeStamp)
		{
			return timeStamp >= this.StartTime && timeStamp < this.EndTime;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001FBCA File Offset: 0x0001DDCA
		public bool Contains(DateTime startDate, DateTime endDate)
		{
			return this.Contains(startDate) || this.Contains(endDate) || (startDate <= this.StartTime && endDate >= this.EndTime);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		public void Drop()
		{
			if (this.DropInternal())
			{
				this.Delete();
			}
			this.expirationAttemptCount++;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		public GenerationCleanupMode Cleanup()
		{
			GenerationCleanupMode generationCleanupMode = this.CleanupInternal();
			if (generationCleanupMode == GenerationCleanupMode.Drop)
			{
				this.Delete();
			}
			this.expirationAttemptCount++;
			return generationCleanupMode;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		public virtual XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement("Generation");
			xelement.Add(new XElement("GenerationId", this.GenerationId));
			xelement.Add(new XElement("Name", this.Name));
			xelement.Add(new XElement("StartTime", this.StartTime));
			xelement.Add(new XElement("EndTime", this.EndTime));
			xelement.Add(new XElement("Duration", this.Duration));
			xelement.Add(new XElement("ExpirationAttemptCount", this.expirationAttemptCount));
			return xelement;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001FD25 File Offset: 0x0001DF25
		internal void Commit()
		{
			this.dataRow.Commit();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001FD32 File Offset: 0x0001DF32
		private void Delete()
		{
			this.dataRow.MarkToDelete();
			this.Commit();
			ExTraceGlobals.StorageTracer.TraceDebug<int>((long)this.GetHashCode(), "Generation Id#{0} deleted from DB", this.GenerationId);
		}

		// Token: 0x040003FC RID: 1020
		private DataGenerationRow dataRow;

		// Token: 0x040003FD RID: 1021
		private int expirationAttemptCount;

		// Token: 0x040003FE RID: 1022
		private bool isAttached;

		// Token: 0x040003FF RID: 1023
		private readonly object attachLock = new object();
	}
}
