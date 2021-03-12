using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000266 RID: 614
	internal abstract class ResourceBase
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x00040AD4 File Offset: 0x0003ECD4
		public ResourceBase()
		{
			this.Reservations = new Dictionary<Guid, ReservationBase>();
			this.TransferRate = new FixedTimeSum(1000, 60);
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06001F07 RID: 7943
		public abstract string ResourceName { get; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06001F08 RID: 7944
		public abstract string ResourceType { get; }

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00040B04 File Offset: 0x0003ED04
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x00040B0C File Offset: 0x0003ED0C
		public ExPerformanceCounter UtilizationPerfCounter { get; protected set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00040B15 File Offset: 0x0003ED15
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x00040B1D File Offset: 0x0003ED1D
		public PerfCounterWithAverageRate TransferRatePerfCounter { get; protected set; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00040B26 File Offset: 0x0003ED26
		public virtual int StaticCapacity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00040B2C File Offset: 0x0003ED2C
		public int Utilization
		{
			get
			{
				int count;
				lock (this.Locker)
				{
					count = this.Reservations.Count;
				}
				return count;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00040B74 File Offset: 0x0003ED74
		public virtual bool IsUnhealthy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00040B77 File Offset: 0x0003ED77
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00040B7F File Offset: 0x0003ED7F
		private protected Dictionary<Guid, ReservationBase> Reservations { protected get; private set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00040B88 File Offset: 0x0003ED88
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00040B90 File Offset: 0x0003ED90
		protected SettingsContextBase ConfigContext { get; set; }

		// Token: 0x06001F14 RID: 7956 RVA: 0x00040B9C File Offset: 0x0003ED9C
		public void Reserve(ReservationBase reservation)
		{
			lock (this.Locker)
			{
				if (!this.Reservations.ContainsKey(reservation.ReservationId))
				{
					this.VerifyCapacity(reservation);
					this.AddReservation(reservation);
					if (this.UtilizationPerfCounter != null)
					{
						this.UtilizationPerfCounter.RawValue = (long)this.Utilization;
					}
				}
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00040C14 File Offset: 0x0003EE14
		public void Charge(uint bytes)
		{
			this.TransferRate.Add(bytes);
			if (this.TransferRatePerfCounter != null)
			{
				this.TransferRatePerfCounter.IncrementBy((long)((ulong)bytes));
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00040C38 File Offset: 0x0003EE38
		public XElement GetDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			string argument = arguments.GetArgument<string>("resources");
			if (!string.IsNullOrEmpty(argument) && !CommonUtils.IsValueInWildcardedList(this.ResourceName, argument) && !CommonUtils.IsValueInWildcardedList(this.ResourceType, argument))
			{
				return null;
			}
			if (arguments.HasArgument("unhealthy") && !this.IsUnhealthy)
			{
				return null;
			}
			return this.GetDiagnosticInfoInternal(arguments);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00040C98 File Offset: 0x0003EE98
		public override string ToString()
		{
			return string.Format("Resource {0}({1}), StaticCapacity {1}, Utilization {2}", new object[]
			{
				this.ResourceType,
				this.ResourceName,
				this.StaticCapacity,
				this.Utilization
			});
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00040CE5 File Offset: 0x0003EEE5
		protected virtual void VerifyStaticCapacity(ReservationBase reservation)
		{
			if (this.Utilization >= this.StaticCapacity)
			{
				this.ThrowStaticCapacityExceededException();
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00040CFB File Offset: 0x0003EEFB
		protected virtual void ThrowStaticCapacityExceededException()
		{
			throw new StaticCapacityExceededReservationException(this.ResourceName, this.ResourceType, this.StaticCapacity);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00040D14 File Offset: 0x0003EF14
		protected virtual void VerifyDynamicCapacity(ReservationBase reservation)
		{
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00040D16 File Offset: 0x0003EF16
		protected virtual void AddReservation(ReservationBase reservation)
		{
			this.Reservations.Add(reservation.ReservationId, reservation);
			reservation.AddReleaseAction(new Action<ReservationBase>(this.ReleaseReservation));
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00040D3C File Offset: 0x0003EF3C
		protected virtual XElement GetDiagnosticInfoInternal(MRSDiagnosticArgument arguments)
		{
			XElement result;
			lock (this.Locker)
			{
				XElement xelement = new XElement("Resource", new object[]
				{
					new XAttribute("Type", this.ResourceType),
					new XAttribute("Name", this.ResourceName),
					new XAttribute("Utilization", this.Utilization)
				});
				foreach (ReservationBase reservationBase in this.Reservations.Values)
				{
					xelement.Add(new XElement("Client", reservationBase.ClientName));
				}
				result = xelement;
			}
			return result;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00040E44 File Offset: 0x0003F044
		private void ReleaseReservation(ReservationBase reservation)
		{
			lock (this.Locker)
			{
				if (this.Reservations.Remove(reservation.ReservationId) && this.UtilizationPerfCounter != null)
				{
					this.UtilizationPerfCounter.RawValue = (long)this.Utilization;
				}
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00040EAC File Offset: 0x0003F0AC
		private void VerifyCapacity(ReservationBase reservation)
		{
			this.VerifyStaticCapacity(reservation);
			this.VerifyDynamicCapacity(reservation);
		}

		// Token: 0x04000C83 RID: 3203
		protected readonly FixedTimeSum TransferRate;

		// Token: 0x04000C84 RID: 3204
		protected readonly object Locker = new object();
	}
}
