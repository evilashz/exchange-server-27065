using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9A RID: 3738
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToMoveMailboxWithSubscriptionsException : RecipientTaskException
	{
		// Token: 0x0600A7DD RID: 42973 RVA: 0x002892A1 File Offset: 0x002874A1
		public UnableToMoveMailboxWithSubscriptionsException(string name) : base(Strings.UnableToMoveMailboxWithSubscriptions(name))
		{
			this.name = name;
		}

		// Token: 0x0600A7DE RID: 42974 RVA: 0x002892B6 File Offset: 0x002874B6
		public UnableToMoveMailboxWithSubscriptionsException(string name, Exception innerException) : base(Strings.UnableToMoveMailboxWithSubscriptions(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A7DF RID: 42975 RVA: 0x002892CC File Offset: 0x002874CC
		protected UnableToMoveMailboxWithSubscriptionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A7E0 RID: 42976 RVA: 0x002892F6 File Offset: 0x002874F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003692 RID: 13970
		// (get) Token: 0x0600A7E1 RID: 42977 RVA: 0x00289311 File Offset: 0x00287511
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005FF8 RID: 24568
		private readonly string name;
	}
}
