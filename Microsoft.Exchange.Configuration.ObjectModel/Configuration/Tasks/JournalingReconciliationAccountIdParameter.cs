using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	public sealed class JournalingReconciliationAccountIdParameter : ADIdParameter
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x00021923 File Offset: 0x0001FB23
		public JournalingReconciliationAccountIdParameter()
		{
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002192B File Offset: 0x0001FB2B
		public JournalingReconciliationAccountIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00021934 File Offset: 0x0001FB34
		public JournalingReconciliationAccountIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002193D File Offset: 0x0001FB3D
		public JournalingReconciliationAccountIdParameter(JournalingReconciliationAccount connector) : base(connector.Id)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002194B File Offset: 0x0001FB4B
		public JournalingReconciliationAccountIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00021954 File Offset: 0x0001FB54
		public static JournalingReconciliationAccountIdParameter Parse(string identity)
		{
			return new JournalingReconciliationAccountIdParameter(identity);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002195C File Offset: 0x0001FB5C
		public JournalingReconciliationAccount GetObject(IConfigDataProvider session)
		{
			IEnumerable<JournalingReconciliationAccount> objects = base.GetObjects<JournalingReconciliationAccount>(null, session);
			IEnumerator<JournalingReconciliationAccount> enumerator = objects.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(this.ToString()));
			}
			JournalingReconciliationAccount result = enumerator.Current;
			if (enumerator.MoveNext())
			{
				throw new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(this.ToString()));
			}
			return result;
		}
	}
}
