using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000168 RID: 360
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExCannotContainWideScopeBindingsException : InvalidCompliancePolicyExBindingException
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00035B41 File Offset: 0x00033D41
		public ExCannotContainWideScopeBindingsException(string binding) : base(Strings.ExCannotContainWideScopeBindings(binding))
		{
			this.binding = binding;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00035B56 File Offset: 0x00033D56
		public ExCannotContainWideScopeBindingsException(string binding, Exception innerException) : base(Strings.ExCannotContainWideScopeBindings(binding), innerException)
		{
			this.binding = binding;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00035B6C File Offset: 0x00033D6C
		protected ExCannotContainWideScopeBindingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.binding = (string)info.GetValue("binding", typeof(string));
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00035B96 File Offset: 0x00033D96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("binding", this.binding);
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00035BB1 File Offset: 0x00033DB1
		public string Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x04000671 RID: 1649
		private readonly string binding;
	}
}
