using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E35 RID: 3637
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidArgumentForServerRoleException : LocalizedException
	{
		// Token: 0x0600A612 RID: 42514 RVA: 0x00287165 File Offset: 0x00285365
		public InvalidArgumentForServerRoleException(string property, string role) : base(Strings.InvalidArgumentForServerRoleException(property, role))
		{
			this.property = property;
			this.role = role;
		}

		// Token: 0x0600A613 RID: 42515 RVA: 0x00287182 File Offset: 0x00285382
		public InvalidArgumentForServerRoleException(string property, string role, Exception innerException) : base(Strings.InvalidArgumentForServerRoleException(property, role), innerException)
		{
			this.property = property;
			this.role = role;
		}

		// Token: 0x0600A614 RID: 42516 RVA: 0x002871A0 File Offset: 0x002853A0
		protected InvalidArgumentForServerRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.role = (string)info.GetValue("role", typeof(string));
		}

		// Token: 0x0600A615 RID: 42517 RVA: 0x002871F5 File Offset: 0x002853F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("role", this.role);
		}

		// Token: 0x1700365B RID: 13915
		// (get) Token: 0x0600A616 RID: 42518 RVA: 0x00287221 File Offset: 0x00285421
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700365C RID: 13916
		// (get) Token: 0x0600A617 RID: 42519 RVA: 0x00287229 File Offset: 0x00285429
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x04005FC1 RID: 24513
		private readonly string property;

		// Token: 0x04005FC2 RID: 24514
		private readonly string role;
	}
}
