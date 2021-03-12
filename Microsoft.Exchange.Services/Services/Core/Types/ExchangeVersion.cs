using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Services.DispatchPipe.Ews;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200076C RID: 1900
	internal class ExchangeVersion : OperatorComparable
	{
		// Token: 0x060038B8 RID: 14520 RVA: 0x000C8948 File Offset: 0x000C6B48
		public ExchangeVersion(ExchangeVersionType version)
		{
			this.version = version;
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000C8958 File Offset: 0x000C6B58
		// (set) Token: 0x060038BA RID: 14522 RVA: 0x000C89AC File Offset: 0x000C6BAC
		public static ExchangeVersion Current
		{
			get
			{
				EwsOperationContextBase operationContext = EWSSettings.GetOperationContext();
				if (ExchangeVersion.CanAccessMessageProperties(operationContext))
				{
					ExchangeVersion result = ExchangeVersion.Exchange2007;
					object obj = null;
					if (operationContext.RequestMessage.Properties.TryGetValue("WS_ServerVersionKey", out obj))
					{
						result = (obj as ExchangeVersion);
					}
					return result;
				}
				return ExchangeVersion.current ?? ExchangeVersion.Latest;
			}
			set
			{
				EwsOperationContextBase operationContext = EWSSettings.GetOperationContext();
				if (ExchangeVersion.CanAccessMessageProperties(operationContext))
				{
					operationContext.IncomingMessageProperties["WS_ServerVersionKey"] = value;
				}
				ExchangeVersion.current = value;
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000C89E0 File Offset: 0x000C6BE0
		internal static ExchangeVersion UnsafeGetCurrent()
		{
			EwsOperationContextBase operationContext = EWSSettings.GetOperationContext();
			if (ExchangeVersion.CanAccessMessageProperties(operationContext) && operationContext.IncomingMessageProperties.ContainsKey("WS_ServerVersionKey"))
			{
				return operationContext.IncomingMessageProperties["WS_ServerVersionKey"] as ExchangeVersion;
			}
			return ExchangeVersion.Exchange2007;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000C8A28 File Offset: 0x000C6C28
		public static ExchangeVersion MapRequestVersionToServerVersion(string versionString)
		{
			if (string.IsNullOrEmpty(versionString))
			{
				return ExchangeVersion.Latest;
			}
			ExchangeVersion result;
			try
			{
				ExchangeVersionType exchangeVersionType = (ExchangeVersionType)Enum.Parse(typeof(ExchangeVersionType), versionString);
				result = new ExchangeVersion(exchangeVersionType);
			}
			catch (ArgumentException)
			{
				throw new InvalidServerVersionException();
			}
			return result;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000C8A7C File Offset: 0x000C6C7C
		public static void ExecuteWithSpecifiedVersion(ExchangeVersion version, ExchangeVersion.ExchangeVersionDelegate versionDelegate)
		{
			ExchangeVersion value = ExchangeVersion.Current;
			try
			{
				ExchangeVersion.Current = version;
				versionDelegate();
			}
			finally
			{
				ExchangeVersion.Current = value;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000C8AB4 File Offset: 0x000C6CB4
		public ExchangeVersionType Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000C8ABC File Offset: 0x000C6CBC
		public bool Supports(ExchangeVersion other)
		{
			return this >= other;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000C8AC5 File Offset: 0x000C6CC5
		public bool Supports(ExchangeVersionType other)
		{
			return this.Version >= other;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000C8AD3 File Offset: 0x000C6CD3
		public override int GetHashCode()
		{
			return this.Version.GetHashCode();
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000C8AE5 File Offset: 0x000C6CE5
		public override int CompareTo(OperatorComparable obj)
		{
			if (obj is ExchangeVersion)
			{
				return this.Version.CompareTo(((ExchangeVersion)obj).Version);
			}
			return -1;
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000C8B11 File Offset: 0x000C6D11
		public override string ToString()
		{
			return this.version.ToString();
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000C8B23 File Offset: 0x000C6D23
		private static bool CanAccessMessageProperties(EwsOperationContextBase operationContext)
		{
			return operationContext != null && operationContext.RequestMessage != null && operationContext.RequestMessage.State != MessageState.Closed && operationContext.IncomingMessageProperties != null;
		}

		// Token: 0x04001F6D RID: 8045
		[ThreadStatic]
		private static ExchangeVersion current;

		// Token: 0x04001F6E RID: 8046
		private ExchangeVersionType version;

		// Token: 0x04001F6F RID: 8047
		public static LazyMember<ExchangeVersionType> MaxSupportedVersion = new LazyMember<ExchangeVersionType>(delegate()
		{
			IEnumerable<ExchangeVersionType> source = Enum.GetValues(typeof(ExchangeVersionType)).Cast<ExchangeVersionType>();
			return source.Max<ExchangeVersionType>();
		});

		// Token: 0x04001F70 RID: 8048
		public static readonly ExchangeVersion Exchange2007 = new ExchangeVersion(ExchangeVersionType.Exchange2007);

		// Token: 0x04001F71 RID: 8049
		public static readonly ExchangeVersion Exchange2007SP1 = new ExchangeVersion(ExchangeVersionType.Exchange2007_SP1);

		// Token: 0x04001F72 RID: 8050
		public static readonly ExchangeVersion Exchange2010 = new ExchangeVersion(ExchangeVersionType.Exchange2010);

		// Token: 0x04001F73 RID: 8051
		public static readonly ExchangeVersion Exchange2010SP1 = new ExchangeVersion(ExchangeVersionType.Exchange2010_SP1);

		// Token: 0x04001F74 RID: 8052
		public static readonly ExchangeVersion Exchange2010SP2 = new ExchangeVersion(ExchangeVersionType.Exchange2010_SP2);

		// Token: 0x04001F75 RID: 8053
		public static readonly ExchangeVersion Exchange2012 = new ExchangeVersion(ExchangeVersionType.Exchange2012);

		// Token: 0x04001F76 RID: 8054
		public static readonly ExchangeVersion Exchange2013 = new ExchangeVersion(ExchangeVersionType.Exchange2013);

		// Token: 0x04001F77 RID: 8055
		public static readonly ExchangeVersion Exchange2013_SP1 = new ExchangeVersion(ExchangeVersionType.Exchange2013_SP1);

		// Token: 0x04001F78 RID: 8056
		public static readonly ExchangeVersion ExchangeV2_1 = new ExchangeVersion(ExchangeVersionType.V2_1);

		// Token: 0x04001F79 RID: 8057
		public static readonly ExchangeVersion ExchangeV2_2 = new ExchangeVersion(ExchangeVersionType.V2_2);

		// Token: 0x04001F7A RID: 8058
		public static readonly ExchangeVersion ExchangeV2_3 = new ExchangeVersion(ExchangeVersionType.V2_3);

		// Token: 0x04001F7B RID: 8059
		public static readonly ExchangeVersion ExchangeV2_4 = new ExchangeVersion(ExchangeVersionType.V2_4);

		// Token: 0x04001F7C RID: 8060
		public static readonly ExchangeVersion ExchangeV2_5 = new ExchangeVersion(ExchangeVersionType.V2_5);

		// Token: 0x04001F7D RID: 8061
		public static readonly ExchangeVersion ExchangeV2_6 = new ExchangeVersion(ExchangeVersionType.V2_6);

		// Token: 0x04001F7E RID: 8062
		public static readonly ExchangeVersion ExchangeV2_14 = new ExchangeVersion(ExchangeVersionType.V2_14);

		// Token: 0x04001F7F RID: 8063
		public static readonly ExchangeVersion Latest = new ExchangeVersion(ExchangeVersion.MaxSupportedVersion.Member);

		// Token: 0x0200076D RID: 1901
		// (Invoke) Token: 0x060038C8 RID: 14536
		public delegate void ExchangeVersionDelegate();
	}
}
