using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000063 RID: 99
	internal class LogTransactionInformationParser
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000EF71 File Offset: 0x0000D171
		public IEnumerable<ILogTransactionInformation> LogTransactionInformationList
		{
			get
			{
				return this.logTransactionInformationList;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ILogTransactionInformation logTransactionInformation in this.logTransactionInformationList)
			{
				stringBuilder.Append(logTransactionInformation.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public void Parse(byte[] buffer)
		{
			if (buffer == null || buffer.Length == 0)
			{
				throw new StoreException((LID)50768U, ErrorCodeValue.InvalidParameter, "No input provided.");
			}
			int i = 0;
			byte b = buffer[i++];
			if (b > LogTransactionInformationCollector.Version)
			{
				throw new StoreException((LID)39504U, ErrorCodeValue.VersionMismatch, string.Format("This parser will not be able to execute on some of the serialized information blocks. Required version is {0}", b));
			}
			while (i < buffer.Length)
			{
				LogTransactionInformationBlockType logTransactionInformationBlockType = (LogTransactionInformationBlockType)buffer[i];
				ILogTransactionInformation logTransactionInformation = null;
				switch (logTransactionInformationBlockType)
				{
				case LogTransactionInformationBlockType.ForTestPurposes:
					logTransactionInformation = new LogTransactionInformationForTestPurposes();
					break;
				case LogTransactionInformationBlockType.Identity:
					logTransactionInformation = new LogTransactionInformationIdentity();
					break;
				case LogTransactionInformationBlockType.AdminRpc:
					logTransactionInformation = new LogTransactionInformationAdmin();
					break;
				case LogTransactionInformationBlockType.MapiRpc:
					logTransactionInformation = new LogTransactionInformationMapi();
					break;
				case LogTransactionInformationBlockType.Task:
					logTransactionInformation = new LogTransactionInformationTask();
					break;
				case LogTransactionInformationBlockType.Digest:
					logTransactionInformation = new LogTransactionInformationDigest();
					break;
				default:
					throw new StoreException((LID)47696U, ErrorCodeValue.InvalidParameter, "Unexpected log transaction information block type.");
				}
				try
				{
					logTransactionInformation.Parse(buffer, ref i);
				}
				catch (ArgumentException ex)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
					throw new StoreException((LID)64080U, ErrorCodeValue.InvalidParameter, "Buffer contains invalid data", ex);
				}
				catch (IndexOutOfRangeException ex2)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
					throw new StoreException((LID)55888U, ErrorCodeValue.InvalidParameter, "Buffer contains invalid data", ex2);
				}
				this.logTransactionInformationList.AddLast(logTransactionInformation);
			}
		}

		// Token: 0x04000205 RID: 517
		private LinkedList<ILogTransactionInformation> logTransactionInformationList = new LinkedList<ILogTransactionInformation>();
	}
}
