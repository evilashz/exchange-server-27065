using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DF RID: 223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSubnetSpecException : ClusCommonValidationFailedException
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0001C658 File Offset: 0x0001A858
		public InvalidSubnetSpecException(string userInput) : base(Strings.InvalidSubnetSpec(userInput))
		{
			this.userInput = userInput;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001C672 File Offset: 0x0001A872
		public InvalidSubnetSpecException(string userInput, Exception innerException) : base(Strings.InvalidSubnetSpec(userInput), innerException)
		{
			this.userInput = userInput;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001C68D File Offset: 0x0001A88D
		protected InvalidSubnetSpecException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userInput = (string)info.GetValue("userInput", typeof(string));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001C6B7 File Offset: 0x0001A8B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userInput", this.userInput);
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001C6D2 File Offset: 0x0001A8D2
		public string UserInput
		{
			get
			{
				return this.userInput;
			}
		}

		// Token: 0x04000738 RID: 1848
		private readonly string userInput;
	}
}
