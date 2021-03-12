using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091F RID: 2335
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _Exception
	{
		// Token: 0x06005F93 RID: 24467
		string ToString();

		// Token: 0x06005F94 RID: 24468
		bool Equals(object obj);

		// Token: 0x06005F95 RID: 24469
		int GetHashCode();

		// Token: 0x06005F96 RID: 24470
		Type GetType();

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005F97 RID: 24471
		string Message { get; }

		// Token: 0x06005F98 RID: 24472
		Exception GetBaseException();

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005F99 RID: 24473
		string StackTrace { get; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06005F9A RID: 24474
		// (set) Token: 0x06005F9B RID: 24475
		string HelpLink { get; set; }

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06005F9C RID: 24476
		// (set) Token: 0x06005F9D RID: 24477
		string Source { get; set; }

		// Token: 0x06005F9E RID: 24478
		[SecurityCritical]
		void GetObjectData(SerializationInfo info, StreamingContext context);

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06005F9F RID: 24479
		Exception InnerException { get; }

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06005FA0 RID: 24480
		MethodBase TargetSite { get; }
	}
}
