using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E22 RID: 3618
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsSharedIdentityTooManyUsersException : LocalizedException
	{
		// Token: 0x0600A5B0 RID: 42416 RVA: 0x00286772 File Offset: 0x00284972
		public RmsSharedIdentityTooManyUsersException(string firstDn, string secondDn) : base(Strings.RmsSharedIdentityTooManyUsers(firstDn, secondDn))
		{
			this.firstDn = firstDn;
			this.secondDn = secondDn;
		}

		// Token: 0x0600A5B1 RID: 42417 RVA: 0x0028678F File Offset: 0x0028498F
		public RmsSharedIdentityTooManyUsersException(string firstDn, string secondDn, Exception innerException) : base(Strings.RmsSharedIdentityTooManyUsers(firstDn, secondDn), innerException)
		{
			this.firstDn = firstDn;
			this.secondDn = secondDn;
		}

		// Token: 0x0600A5B2 RID: 42418 RVA: 0x002867B0 File Offset: 0x002849B0
		protected RmsSharedIdentityTooManyUsersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.firstDn = (string)info.GetValue("firstDn", typeof(string));
			this.secondDn = (string)info.GetValue("secondDn", typeof(string));
		}

		// Token: 0x0600A5B3 RID: 42419 RVA: 0x00286805 File Offset: 0x00284A05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("firstDn", this.firstDn);
			info.AddValue("secondDn", this.secondDn);
		}

		// Token: 0x17003645 RID: 13893
		// (get) Token: 0x0600A5B4 RID: 42420 RVA: 0x00286831 File Offset: 0x00284A31
		public string FirstDn
		{
			get
			{
				return this.firstDn;
			}
		}

		// Token: 0x17003646 RID: 13894
		// (get) Token: 0x0600A5B5 RID: 42421 RVA: 0x00286839 File Offset: 0x00284A39
		public string SecondDn
		{
			get
			{
				return this.secondDn;
			}
		}

		// Token: 0x04005FAB RID: 24491
		private readonly string firstDn;

		// Token: 0x04005FAC RID: 24492
		private readonly string secondDn;
	}
}
