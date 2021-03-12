using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000039 RID: 57
	internal class ObjectNotInitializedException : ApplicationException
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000C52B File Offset: 0x0000A72B
		internal ObjectNotInitializedException(Type type) : base(string.Format("An instance of {0} is used before initialization.", type))
		{
		}
	}
}
