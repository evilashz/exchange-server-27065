using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000841 RID: 2113
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005AB0 RID: 23216 RVA: 0x0013D6BD File Offset: 0x0013B8BD
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm) : base(h, mcm)
		{
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x0013D6C7 File Offset: 0x0013B8C7
		internal ConstructionResponse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x0013D6D4 File Offset: 0x0013B8D4
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}
	}
}
