using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E39 RID: 3641
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExInvalidArgumentForServerRoleException : LocalizedException
	{
		// Token: 0x0600A629 RID: 42537 RVA: 0x00287441 File Offset: 0x00285641
		public ExInvalidArgumentForServerRoleException(string property, string role) : base(Strings.ExInvalidArgumentForServerRoleException(property, role))
		{
			this.property = property;
			this.role = role;
		}

		// Token: 0x0600A62A RID: 42538 RVA: 0x0028745E File Offset: 0x0028565E
		public ExInvalidArgumentForServerRoleException(string property, string role, Exception innerException) : base(Strings.ExInvalidArgumentForServerRoleException(property, role), innerException)
		{
			this.property = property;
			this.role = role;
		}

		// Token: 0x0600A62B RID: 42539 RVA: 0x0028747C File Offset: 0x0028567C
		protected ExInvalidArgumentForServerRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.role = (string)info.GetValue("role", typeof(string));
		}

		// Token: 0x0600A62C RID: 42540 RVA: 0x002874D1 File Offset: 0x002856D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("role", this.role);
		}

		// Token: 0x17003662 RID: 13922
		// (get) Token: 0x0600A62D RID: 42541 RVA: 0x002874FD File Offset: 0x002856FD
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17003663 RID: 13923
		// (get) Token: 0x0600A62E RID: 42542 RVA: 0x00287505 File Offset: 0x00285705
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x04005FC8 RID: 24520
		private readonly string property;

		// Token: 0x04005FC9 RID: 24521
		private readonly string role;
	}
}
