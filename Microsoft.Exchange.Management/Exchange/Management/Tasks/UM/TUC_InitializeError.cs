using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011EE RID: 4590
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_InitializeError : LocalizedException
	{
		// Token: 0x0600B981 RID: 47489 RVA: 0x002A6038 File Offset: 0x002A4238
		public TUC_InitializeError(string error) : base(Strings.InitializeError(error))
		{
			this.error = error;
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x002A604D File Offset: 0x002A424D
		public TUC_InitializeError(string error, Exception innerException) : base(Strings.InitializeError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600B983 RID: 47491 RVA: 0x002A6063 File Offset: 0x002A4263
		protected TUC_InitializeError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B984 RID: 47492 RVA: 0x002A608D File Offset: 0x002A428D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003A46 RID: 14918
		// (get) Token: 0x0600B985 RID: 47493 RVA: 0x002A60A8 File Offset: 0x002A42A8
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006461 RID: 25697
		private readonly string error;
	}
}
