using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000148 RID: 328
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingExpectedCapabilityException : MigrationPermanentException
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x0006E841 File Offset: 0x0006CA41
		public MissingExpectedCapabilityException(string user, string capability) : base(Strings.ErrorMissingExpectedCapability(user, capability))
		{
			this.user = user;
			this.capability = capability;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0006E85E File Offset: 0x0006CA5E
		public MissingExpectedCapabilityException(string user, string capability, Exception innerException) : base(Strings.ErrorMissingExpectedCapability(user, capability), innerException)
		{
			this.user = user;
			this.capability = capability;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0006E87C File Offset: 0x0006CA7C
		protected MissingExpectedCapabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.capability = (string)info.GetValue("capability", typeof(string));
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0006E8D1 File Offset: 0x0006CAD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("capability", this.capability);
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0006E8FD File Offset: 0x0006CAFD
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0006E905 File Offset: 0x0006CB05
		public string Capability
		{
			get
			{
				return this.capability;
			}
		}

		// Token: 0x04000ADB RID: 2779
		private readonly string user;

		// Token: 0x04000ADC RID: 2780
		private readonly string capability;
	}
}
