using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.Services.Common
{
	// Token: 0x02000257 RID: 599
	internal class TimeZoneException : LocalizedException
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x0004E918 File Offset: 0x0004CB18
		public TimeZoneException(Exception exception) : base(Strings.GetLocalizedString(Strings.IDs.ErrorTimeZone), exception)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004E936 File Offset: 0x0004CB36
		public TimeZoneException(Enum messageId) : base(Strings.GetLocalizedString((Strings.IDs)messageId))
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004E954 File Offset: 0x0004CB54
		public TimeZoneException(Enum messageId, Exception exception) : base(Strings.GetLocalizedString((Strings.IDs)messageId), exception)
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004E973 File Offset: 0x0004CB73
		public TimeZoneException(Enum messageId, Exception innerException, TimeZoneDefinition.TransitionsGroup transitionsGroup, TimeZoneDefinition.Transition transition, string param, string paramValue) : this(messageId, innerException)
		{
			this.AddParam(param, paramValue);
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004E990 File Offset: 0x0004CB90
		public TimeZoneException(Enum messageId, Exception innerException, TimeZoneDefinition.TransitionsGroup transitionsGroup, TimeZoneDefinition.Transition transition) : this(messageId, innerException)
		{
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004E9A3 File Offset: 0x0004CBA3
		public TimeZoneException(Enum messageId, TimeZoneDefinition.TransitionsGroup transitionsGroup, TimeZoneDefinition.Transition transition) : this(messageId)
		{
			this.AddTransitionReferenceInfo(transition, transitionsGroup);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004E9B4 File Offset: 0x0004CBB4
		public TimeZoneException(Enum messageId, string[] paramNames, string[] paramValues) : this(messageId)
		{
			this.AddParams(paramNames, paramValues);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0004E9C5 File Offset: 0x0004CBC5
		public IDictionary<string, string> ConstantValues
		{
			get
			{
				return this.constantValues;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0004E9CD File Offset: 0x0004CBCD
		private static string TransitionTypeToString(TimeZoneDefinition.Transition transition)
		{
			if (transition != null)
			{
				transition.GetType();
				return transition.GetType().Name;
			}
			return "UnknownTansition";
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004E9EA File Offset: 0x0004CBEA
		private void AddTransitionReferenceInfo(TimeZoneDefinition.TransitionsGroup transitionsGroup)
		{
			if (!string.IsNullOrEmpty(transitionsGroup.Id))
			{
				this.AddParam(transitionsGroup.Name + ".Id", transitionsGroup.Id);
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004EA18 File Offset: 0x0004CC18
		private void AddTransitionReferenceInfo(TimeZoneDefinition.Transition transition, TimeZoneDefinition.TransitionsGroup transitionsGroup)
		{
			string text = transitionsGroup.Name + "." + TimeZoneException.TransitionTypeToString(transition) + ".To";
			this.AddTransitionReferenceInfo(transitionsGroup);
			this.AddParam(text + ".Kind", transition.To.Kind.ToString());
			this.AddParam(text, transition.To.Value);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0004EA80 File Offset: 0x0004CC80
		private void AddParam(string paramName, string paramValue)
		{
			if (paramName != null && !this.ConstantValues.ContainsKey(paramName))
			{
				this.ConstantValues.Add(paramName, paramValue);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
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

		// Token: 0x04000B8F RID: 2959
		private Dictionary<string, string> constantValues = new Dictionary<string, string>();
	}
}
