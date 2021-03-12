using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A3 RID: 2211
	internal class TimeZoneException : LocalizedException
	{
		// Token: 0x06003EEE RID: 16110 RVA: 0x000D9DBE File Offset: 0x000D7FBE
		public TimeZoneException(Exception exception) : base(Strings.GetLocalizedString(Strings.IDs.ErrorTimeZone), exception)
		{
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000D9DDC File Offset: 0x000D7FDC
		public TimeZoneException(Enum messageId) : base(Strings.GetLocalizedString((Strings.IDs)messageId))
		{
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000D9DFA File Offset: 0x000D7FFA
		public TimeZoneException(Enum messageId, Exception exception) : base(Strings.GetLocalizedString((Strings.IDs)messageId), exception)
		{
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000D9E19 File Offset: 0x000D8019
		public TimeZoneException(Enum messageId, Exception innerException, ArrayOfTransitionsType transitionsGroup, TransitionType transition, string param, string paramValue) : this(messageId, innerException)
		{
			this.AddParam(param, paramValue);
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000D9E36 File Offset: 0x000D8036
		public TimeZoneException(Enum messageId, Exception innerException, ArrayOfTransitionsType transitionsGroup, TransitionType transition) : this(messageId, innerException)
		{
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000D9E49 File Offset: 0x000D8049
		public TimeZoneException(Enum messageId, ArrayOfTransitionsType transitionsGroup, TransitionType transition) : this(messageId)
		{
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000D9E5A File Offset: 0x000D805A
		public TimeZoneException(Enum messageId, string[] paramNames, string[] paramValues) : this(messageId)
		{
			this.AddParams(paramNames, paramValues);
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000D9E6B File Offset: 0x000D806B
		public IDictionary<string, string> ConstantValues
		{
			get
			{
				return this.constantValues;
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000D9E73 File Offset: 0x000D8073
		private static string TransitionTypeToString(TransitionType transition)
		{
			if (transition != null)
			{
				transition.GetType();
				return transition.GetType().Name;
			}
			return "UnknownTansition";
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000D9E90 File Offset: 0x000D8090
		private void AddTransitionReferenceInfo(ArrayOfTransitionsType transitionsGroup)
		{
			if (!string.IsNullOrEmpty(transitionsGroup.Id))
			{
				this.AddParam(transitionsGroup.Name + ".Id", transitionsGroup.Id);
			}
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000D9EBC File Offset: 0x000D80BC
		private void AddTransitionReferenceInfo(TransitionType transition, ArrayOfTransitionsType transitionsGroup)
		{
			string text = transitionsGroup.Name + "." + TimeZoneException.TransitionTypeToString(transition) + ".To";
			this.AddTransitionReferenceInfo(transitionsGroup);
			this.AddParam(text + ".Kind", transition.To.Kind.ToString());
			this.AddParam(text, transition.To.Value);
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000D9F24 File Offset: 0x000D8124
		private void AddParam(string paramName, string paramValue)
		{
			if (paramName != null && !this.ConstantValues.ContainsKey(paramName))
			{
				this.ConstantValues.Add(paramName, paramValue);
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x000D9F44 File Offset: 0x000D8144
		private void AddParams(string[] paramNames, string[] paramValues)
		{
			if (paramNames != null)
			{
				for (int i = 0; i < paramNames.Length; i++)
				{
					if (paramNames[i] != null && !this.ConstantValues.ContainsKey(paramNames[i]))
					{
						this.ConstantValues.Add(paramNames[i], (i < paramValues.Length) ? paramValues[i] : null);
					}
				}
			}
		}

		// Token: 0x0400242C RID: 9260
		private Dictionary<string, string> constantValues = new Dictionary<string, string>();
	}
}
