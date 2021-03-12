using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000027 RID: 39
	internal sealed class PriorityScope : IMessageScope, IEquatable<IMessageScope>, IEquatable<PriorityScope>
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004533 File Offset: 0x00002733
		public PriorityScope(DeliveryPriority priority)
		{
			ArgumentValidator.ThrowIfNull("priority", priority);
			this.priority = priority;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004552 File Offset: 0x00002752
		public string Display
		{
			get
			{
				return "Priority:" + this.Value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004564 File Offset: 0x00002764
		public MessageScopeType Type
		{
			get
			{
				return MessageScopeType.Priority;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004567 File Offset: 0x00002767
		public object Value
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004574 File Offset: 0x00002774
		public static bool operator ==(PriorityScope left, PriorityScope right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000457D File Offset: 0x0000277D
		public static bool operator !=(PriorityScope left, PriorityScope right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004589 File Offset: 0x00002789
		public bool Equals(PriorityScope other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || this.priority == other.priority);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000045AF File Offset: 0x000027AF
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((PriorityScope)obj)));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000045E8 File Offset: 0x000027E8
		public override int GetHashCode()
		{
			return (int)this.priority;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000045F0 File Offset: 0x000027F0
		public bool Equals(IMessageScope other)
		{
			return this.Equals(other as PriorityScope);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000045FE File Offset: 0x000027FE
		public override string ToString()
		{
			return this.Display;
		}

		// Token: 0x0400006E RID: 110
		private readonly DeliveryPriority priority;
	}
}
