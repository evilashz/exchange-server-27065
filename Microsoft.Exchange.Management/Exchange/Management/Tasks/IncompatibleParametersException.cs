using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8C RID: 3724
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncompatibleParametersException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A793 RID: 42899 RVA: 0x00288A9D File Offset: 0x00286C9D
		public IncompatibleParametersException(string paramName1, string paramName2) : base(Strings.ErrorIncompatibleParameters(paramName1, paramName2))
		{
			this.paramName1 = paramName1;
			this.paramName2 = paramName2;
		}

		// Token: 0x0600A794 RID: 42900 RVA: 0x00288ABA File Offset: 0x00286CBA
		public IncompatibleParametersException(string paramName1, string paramName2, Exception innerException) : base(Strings.ErrorIncompatibleParameters(paramName1, paramName2), innerException)
		{
			this.paramName1 = paramName1;
			this.paramName2 = paramName2;
		}

		// Token: 0x0600A795 RID: 42901 RVA: 0x00288AD8 File Offset: 0x00286CD8
		protected IncompatibleParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.paramName1 = (string)info.GetValue("paramName1", typeof(string));
			this.paramName2 = (string)info.GetValue("paramName2", typeof(string));
		}

		// Token: 0x0600A796 RID: 42902 RVA: 0x00288B2D File Offset: 0x00286D2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("paramName1", this.paramName1);
			info.AddValue("paramName2", this.paramName2);
		}

		// Token: 0x17003680 RID: 13952
		// (get) Token: 0x0600A797 RID: 42903 RVA: 0x00288B59 File Offset: 0x00286D59
		public string ParamName1
		{
			get
			{
				return this.paramName1;
			}
		}

		// Token: 0x17003681 RID: 13953
		// (get) Token: 0x0600A798 RID: 42904 RVA: 0x00288B61 File Offset: 0x00286D61
		public string ParamName2
		{
			get
			{
				return this.paramName2;
			}
		}

		// Token: 0x04005FE6 RID: 24550
		private readonly string paramName1;

		// Token: 0x04005FE7 RID: 24551
		private readonly string paramName2;
	}
}
