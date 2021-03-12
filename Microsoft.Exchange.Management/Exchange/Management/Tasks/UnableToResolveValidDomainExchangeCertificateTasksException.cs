using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E13 RID: 3603
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToResolveValidDomainExchangeCertificateTasksException : LocalizedException
	{
		// Token: 0x0600A55F RID: 42335 RVA: 0x00285E68 File Offset: 0x00284068
		public UnableToResolveValidDomainExchangeCertificateTasksException(string hostName, string physicalName, string fullyQualifiedName, string physicalFullyQualifiedName) : base(Strings.UnableToResolveValidDomainExchangeCertificateTasksException(hostName, physicalName, fullyQualifiedName, physicalFullyQualifiedName))
		{
			this.hostName = hostName;
			this.physicalName = physicalName;
			this.fullyQualifiedName = fullyQualifiedName;
			this.physicalFullyQualifiedName = physicalFullyQualifiedName;
		}

		// Token: 0x0600A560 RID: 42336 RVA: 0x00285E97 File Offset: 0x00284097
		public UnableToResolveValidDomainExchangeCertificateTasksException(string hostName, string physicalName, string fullyQualifiedName, string physicalFullyQualifiedName, Exception innerException) : base(Strings.UnableToResolveValidDomainExchangeCertificateTasksException(hostName, physicalName, fullyQualifiedName, physicalFullyQualifiedName), innerException)
		{
			this.hostName = hostName;
			this.physicalName = physicalName;
			this.fullyQualifiedName = fullyQualifiedName;
			this.physicalFullyQualifiedName = physicalFullyQualifiedName;
		}

		// Token: 0x0600A561 RID: 42337 RVA: 0x00285EC8 File Offset: 0x002840C8
		protected UnableToResolveValidDomainExchangeCertificateTasksException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hostName = (string)info.GetValue("hostName", typeof(string));
			this.physicalName = (string)info.GetValue("physicalName", typeof(string));
			this.fullyQualifiedName = (string)info.GetValue("fullyQualifiedName", typeof(string));
			this.physicalFullyQualifiedName = (string)info.GetValue("physicalFullyQualifiedName", typeof(string));
		}

		// Token: 0x0600A562 RID: 42338 RVA: 0x00285F60 File Offset: 0x00284160
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hostName", this.hostName);
			info.AddValue("physicalName", this.physicalName);
			info.AddValue("fullyQualifiedName", this.fullyQualifiedName);
			info.AddValue("physicalFullyQualifiedName", this.physicalFullyQualifiedName);
		}

		// Token: 0x17003630 RID: 13872
		// (get) Token: 0x0600A563 RID: 42339 RVA: 0x00285FB9 File Offset: 0x002841B9
		public string HostName
		{
			get
			{
				return this.hostName;
			}
		}

		// Token: 0x17003631 RID: 13873
		// (get) Token: 0x0600A564 RID: 42340 RVA: 0x00285FC1 File Offset: 0x002841C1
		public string PhysicalName
		{
			get
			{
				return this.physicalName;
			}
		}

		// Token: 0x17003632 RID: 13874
		// (get) Token: 0x0600A565 RID: 42341 RVA: 0x00285FC9 File Offset: 0x002841C9
		public string FullyQualifiedName
		{
			get
			{
				return this.fullyQualifiedName;
			}
		}

		// Token: 0x17003633 RID: 13875
		// (get) Token: 0x0600A566 RID: 42342 RVA: 0x00285FD1 File Offset: 0x002841D1
		public string PhysicalFullyQualifiedName
		{
			get
			{
				return this.physicalFullyQualifiedName;
			}
		}

		// Token: 0x04005F96 RID: 24470
		private readonly string hostName;

		// Token: 0x04005F97 RID: 24471
		private readonly string physicalName;

		// Token: 0x04005F98 RID: 24472
		private readonly string fullyQualifiedName;

		// Token: 0x04005F99 RID: 24473
		private readonly string physicalFullyQualifiedName;
	}
}
