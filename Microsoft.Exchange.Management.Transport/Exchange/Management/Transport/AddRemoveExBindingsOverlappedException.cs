using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000160 RID: 352
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AddRemoveExBindingsOverlappedException : InvalidCompliancePolicyExBindingException
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x000358F6 File Offset: 0x00033AF6
		public AddRemoveExBindingsOverlappedException(string bindings) : base(Strings.ErrorAddRemoveExBindingsOverlapped(bindings))
		{
			this.bindings = bindings;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003590B File Offset: 0x00033B0B
		public AddRemoveExBindingsOverlappedException(string bindings, Exception innerException) : base(Strings.ErrorAddRemoveExBindingsOverlapped(bindings), innerException)
		{
			this.bindings = bindings;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00035921 File Offset: 0x00033B21
		protected AddRemoveExBindingsOverlappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bindings = (string)info.GetValue("bindings", typeof(string));
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003594B File Offset: 0x00033B4B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bindings", this.bindings);
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00035966 File Offset: 0x00033B66
		public string Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0400066E RID: 1646
		private readonly string bindings;
	}
}
