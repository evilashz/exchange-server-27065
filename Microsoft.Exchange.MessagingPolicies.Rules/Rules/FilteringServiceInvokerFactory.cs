using System;
using System.IO;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001A RID: 26
	internal class FilteringServiceInvokerFactory
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004514 File Offset: 0x00002714
		public static FilteringServiceInvoker Create(ITracer tracer)
		{
			FilteringServiceInvoker result;
			try
			{
				result = new FipsFilteringServiceInvoker();
			}
			catch (Exception e)
			{
				FilteringServiceInvokerFactory.LogFipsInvokerCreationException(e, tracer);
				result = NullFilteringServiceInvoker.Factory();
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000454C File Offset: 0x0000274C
		protected static void LogFipsInvokerCreationException(Exception e, ITracer tracer)
		{
			if (e is FileNotFoundException)
			{
				tracer.TraceWarning("Fips does not appear to be installed. Acting as if there are no fips results (Exception: '{0}')", new object[]
				{
					e
				});
				return;
			}
			tracer.TraceError("Unexpected exception while creating FipsFilteringServiceInvoker (Exception: '{0}')", new object[]
			{
				e
			});
		}
	}
}
