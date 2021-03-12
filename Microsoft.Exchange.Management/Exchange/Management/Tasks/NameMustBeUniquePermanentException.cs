using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC2 RID: 3778
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NameMustBeUniquePermanentException : ManagementObjectAlreadyExistsException
	{
		// Token: 0x0600A8A2 RID: 43170 RVA: 0x0028A509 File Offset: 0x00288709
		public NameMustBeUniquePermanentException(string name, string mbx) : base(Strings.ErrorNameMustBeUniquePerMailbox(name, mbx))
		{
			this.name = name;
			this.mbx = mbx;
		}

		// Token: 0x0600A8A3 RID: 43171 RVA: 0x0028A526 File Offset: 0x00288726
		public NameMustBeUniquePermanentException(string name, string mbx, Exception innerException) : base(Strings.ErrorNameMustBeUniquePerMailbox(name, mbx), innerException)
		{
			this.name = name;
			this.mbx = mbx;
		}

		// Token: 0x0600A8A4 RID: 43172 RVA: 0x0028A544 File Offset: 0x00288744
		protected NameMustBeUniquePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.mbx = (string)info.GetValue("mbx", typeof(string));
		}

		// Token: 0x0600A8A5 RID: 43173 RVA: 0x0028A599 File Offset: 0x00288799
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("mbx", this.mbx);
		}

		// Token: 0x170036B7 RID: 14007
		// (get) Token: 0x0600A8A6 RID: 43174 RVA: 0x0028A5C5 File Offset: 0x002887C5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170036B8 RID: 14008
		// (get) Token: 0x0600A8A7 RID: 43175 RVA: 0x0028A5CD File Offset: 0x002887CD
		public string Mbx
		{
			get
			{
				return this.mbx;
			}
		}

		// Token: 0x0400601D RID: 24605
		private readonly string name;

		// Token: 0x0400601E RID: 24606
		private readonly string mbx;
	}
}
