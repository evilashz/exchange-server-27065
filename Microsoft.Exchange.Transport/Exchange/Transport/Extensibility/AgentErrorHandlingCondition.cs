using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000305 RID: 773
	internal class AgentErrorHandlingCondition
	{
		// Token: 0x060021BE RID: 8638 RVA: 0x0007FD10 File Offset: 0x0007DF10
		public AgentErrorHandlingCondition(string contextId, Type exceptionType, int errorDeferCount = 0, string exceptionMessage = null)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			if (exceptionType == null)
			{
				throw new ArgumentNullException("exceptionType");
			}
			this.ContextId = contextId;
			this.ExceptionTypeName = null;
			this.exceptionType = exceptionType;
			this.exceptionTypes = null;
			this.ExceptionMessage = exceptionMessage;
			this.ErrorDeferCount = errorDeferCount;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0007FD70 File Offset: 0x0007DF70
		public AgentErrorHandlingCondition(string contextId, IEnumerable<Type> exceptionTypes, int errorDeferCount = 0, string exceptionMessage = null)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			if (exceptionTypes == null || exceptionTypes.Contains(null))
			{
				throw new ArgumentNullException("exceptionTypes");
			}
			this.ContextId = contextId;
			this.ExceptionTypeName = null;
			this.exceptionType = null;
			this.exceptionTypes = exceptionTypes;
			this.ExceptionMessage = exceptionMessage;
			this.ErrorDeferCount = errorDeferCount;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0007FDD4 File Offset: 0x0007DFD4
		public AgentErrorHandlingCondition(string contextId, string exceptionTypeName, int errorDeferCount = 0, string exceptionMessage = null)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			if (exceptionTypeName == null)
			{
				throw new ArgumentNullException("exceptionTypeName");
			}
			this.ContextId = contextId;
			this.exceptionType = null;
			this.exceptionTypes = null;
			this.ExceptionTypeName = exceptionTypeName;
			this.ExceptionMessage = exceptionMessage;
			this.ErrorDeferCount = errorDeferCount;
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0007FE2E File Offset: 0x0007E02E
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0007FE36 File Offset: 0x0007E036
		public string ContextId { get; private set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0007FE3F File Offset: 0x0007E03F
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x0007FE47 File Offset: 0x0007E047
		public string ExceptionTypeName { get; private set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0007FE50 File Offset: 0x0007E050
		public Type ExceptionType
		{
			get
			{
				if (this.exceptionType == null && this.exceptionTypes == null && this.ExceptionTypeName != null)
				{
					this.exceptionType = ((this.ExceptionTypeName == string.Empty) ? typeof(Exception) : Type.GetType(this.ExceptionTypeName));
				}
				return this.exceptionType;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0007FEBC File Offset: 0x0007E0BC
		public IEnumerable<Type> ExceptionTypes
		{
			get
			{
				if (this.exceptionTypes == null && this.exceptionType == null && this.ExceptionTypeName != null)
				{
					string[] source = this.ExceptionTypeName.Split(new char[]
					{
						';'
					});
					if (source.Any<string>())
					{
						Type[] source2 = (from t in source.Select(new Func<string, Type>(Type.GetType))
						where t != null
						select t).ToArray<Type>();
						if (source2.Any<Type>())
						{
							this.exceptionTypes = source2;
						}
					}
				}
				return this.exceptionTypes;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0007FF5A File Offset: 0x0007E15A
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0007FF62 File Offset: 0x0007E162
		public string ExceptionMessage { get; private set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0007FF6B File Offset: 0x0007E16B
		// (set) Token: 0x060021CA RID: 8650 RVA: 0x0007FF73 File Offset: 0x0007E173
		public int ErrorDeferCount { get; private set; }

		// Token: 0x060021CB RID: 8651 RVA: 0x0007FFAC File Offset: 0x0007E1AC
		public bool IsMatch(string contextId, Exception exception, TransportMailItem mailItem)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			return (this.ContextId == "*" || this.ContextId == contextId) && ((this.ExceptionTypes != null && this.ExceptionTypes.Any((Type t) => exception.GetType() == t || exception.GetType().IsSubclassOf(t))) || (this.ExceptionType != null && (exception.GetType() == this.ExceptionType || exception.GetType().IsSubclassOf(this.ExceptionType)))) && (string.IsNullOrEmpty(this.ExceptionMessage) || exception.Message.IndexOf(this.ExceptionMessage, StringComparison.InvariantCultureIgnoreCase) >= 0) && (this.ErrorDeferCount == 0 || (mailItem != null && mailItem.ExtendedProperties.GetValue<int>("Microsoft.Exchange.Transport.AgentErrorDeferCount", 0) >= this.ErrorDeferCount));
		}

		// Token: 0x040011B1 RID: 4529
		private Type exceptionType;

		// Token: 0x040011B2 RID: 4530
		private IEnumerable<Type> exceptionTypes;
	}
}
