using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200013E RID: 318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RunspaceAccessorBase : SubscriptionAccessorBase
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x00043FC1 File Offset: 0x000421C1
		protected RunspaceAccessorBase(IMigrationDataProvider dataProvider)
		{
			this.DataProvider = dataProvider;
			this.OrganizationId = dataProvider.OrganizationId;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00043FDC File Offset: 0x000421DC
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x00043FE4 File Offset: 0x000421E4
		internal IMigrationDataProvider DataProvider { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00043FED File Offset: 0x000421ED
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00043FF5 File Offset: 0x000421F5
		internal OrganizationId OrganizationId { get; private set; }

		// Token: 0x06001003 RID: 4099 RVA: 0x00043FFE File Offset: 0x000421FE
		internal T RunCommand<T>(PSCommand command) where T : class
		{
			return this.RunCommand<T>(command, null, null);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004400C File Offset: 0x0004220C
		internal T RunCommand<T>(PSCommand command, ICollection<Type> ignoreExceptions, ICollection<Type> transientExceptions) where T : class
		{
			Type type;
			return this.RunCommand<T>(command, ignoreExceptions, transientExceptions, out type);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00044024 File Offset: 0x00042224
		internal T RunCommand<T>(PSCommand command, ICollection<Type> ignoreExceptions, ICollection<Type> transientExceptions, out Type ignoredErrorType) where T : class
		{
			ErrorRecord errorRecord = null;
			ignoredErrorType = null;
			string commandString = MigrationRunspaceProxy.GetCommandString(command);
			try
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Running PS command {0}", new object[]
				{
					commandString
				});
				T result = this.DataProvider.RunspaceProxy.RunPSCommand<T>(command, out errorRecord);
				if (errorRecord == null)
				{
					return result;
				}
			}
			catch (ParameterBindingException ex)
			{
				return this.HandleException<T>(commandString, ex, ignoreExceptions, transientExceptions, out ignoredErrorType);
			}
			catch (CmdletInvocationException ex2)
			{
				return this.HandleException<T>(commandString, ex2.InnerException ?? ex2, ignoreExceptions, transientExceptions, out ignoredErrorType);
			}
			MigrationUtil.AssertOrThrow(errorRecord != null, "expect to have an error at this point", new object[0]);
			if (errorRecord.Exception != null)
			{
				return this.HandleException<T>(commandString, errorRecord.Exception, ignoreExceptions, transientExceptions, out ignoredErrorType);
			}
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, errorRecord.ToString()));
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00044108 File Offset: 0x00042308
		protected static bool ExceptionIsIn(Exception ex, ICollection<Type> typesToCheck, out Type exceptionType)
		{
			if (typesToCheck != null)
			{
				Type type = ex.GetType();
				foreach (Type type2 in typesToCheck)
				{
					if (type2.IsAssignableFrom(type))
					{
						exceptionType = type2;
						return true;
					}
				}
			}
			exceptionType = null;
			return false;
		}

		// Token: 0x06001007 RID: 4103
		protected abstract T HandleException<T>(string commandString, Exception ex, ICollection<Type> transientExceptions);

		// Token: 0x06001008 RID: 4104 RVA: 0x0004416C File Offset: 0x0004236C
		private T HandleException<T>(string commandString, Exception ex, ICollection<Type> ignoreExceptions, ICollection<Type> transientExceptions, out Type ignoredErrorType) where T : class
		{
			MigrationUtil.ThrowOnNullArgument(ex, "ex");
			Type type;
			if (RunspaceAccessorBase.ExceptionIsIn(ex, ignoreExceptions, out type))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "ignoring exception because it's on ignored list", new object[]
				{
					ex
				});
				ignoredErrorType = type;
				return default(T);
			}
			ignoredErrorType = null;
			return this.HandleException<T>(commandString, ex, transientExceptions);
		}
	}
}
