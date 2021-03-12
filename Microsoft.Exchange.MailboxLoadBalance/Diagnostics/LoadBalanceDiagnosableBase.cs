using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class LoadBalanceDiagnosableBase<TArgument, TResult> : DataContractDiagnosableBase<TArgument> where TArgument : LoadBalanceDiagnosableArgumentBase, new()
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00009A97 File Offset: 0x00007C97
		protected LoadBalanceDiagnosableBase(ILogger logger)
		{
			this.currentLogger = logger;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00009AA6 File Offset: 0x00007CA6
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00009AAE File Offset: 0x00007CAE
		private protected DiagnosticLogger Logger { protected get; private set; }

		// Token: 0x0600032A RID: 810 RVA: 0x00009AB8 File Offset: 0x00007CB8
		protected override object GetDiagnosticResult()
		{
			this.Logger = new DiagnosticLogger(this.currentLogger);
			TResult tresult = this.ProcessDiagnostic();
			TArgument arguments = base.Arguments;
			if (arguments.TraceEnabled)
			{
				return new TraceDecoratedResult
				{
					Logs = this.Logger.Logs,
					Result = tresult
				};
			}
			return tresult;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009B28 File Offset: 0x00007D28
		protected override XmlObjectSerializer CreateSerializer(Type type)
		{
			return new DataContractSerializer(type, type.Name, type.Namespace ?? string.Empty, Array<Type>.Empty, int.MaxValue, false, true, new LoadBalanceDataContractSurrogate(), this.CreateDataContractResolver());
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009B67 File Offset: 0x00007D67
		protected override DataContractResolver CreateDataContractResolver()
		{
			return new LoadBalanceDataContractResolver();
		}

		// Token: 0x0600032D RID: 813
		protected abstract TResult ProcessDiagnostic();

		// Token: 0x040000EA RID: 234
		private readonly ILogger currentLogger;
	}
}
