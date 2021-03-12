using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B32 RID: 2866
	internal static class InterceptorHelper
	{
		// Token: 0x060066FC RID: 26364 RVA: 0x001A983C File Offset: 0x001A7A3C
		internal static bool ValidateEventConditionPairs(InterceptorAgentEvent evt, List<InterceptorAgentCondition> conditions, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			using (IEnumerator<KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent>> enumerator = InterceptorAgentRule.InvalidConditionEventPairs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> pair = enumerator.Current;
					InterceptorAgentEvent evt2 = evt;
					KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> pair5 = pair;
					if (evt2 == pair5.Value)
					{
						if (conditions.Exists(delegate(InterceptorAgentCondition c)
						{
							InterceptorAgentConditionType property = c.Property;
							KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> pair4 = pair;
							return property == pair4.Key;
						}))
						{
							IEnumerable<string> values = InterceptorAgentCondition.AllConditions.Except(new string[]
							{
								InterceptorAgentConditionType.Invalid.ToString()
							}).Except(from p in InterceptorAgentRule.InvalidConditionEventPairs
							where p.Value == evt
							select p.Key.ToString());
							KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> pair2 = pair;
							string evt3 = pair2.Value.ToString();
							KeyValuePair<InterceptorAgentConditionType, InterceptorAgentEvent> pair3 = pair;
							error = Strings.InterceptorErrorInvalidEventConditionPair(evt3, pair3.Key.ToString(), string.Join(",", values));
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x001A99E0 File Offset: 0x001A7BE0
		internal static bool ValidateEventActionPairs(InterceptorAgentEvent evt, InterceptorAgentRuleBehavior action, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			foreach (KeyValuePair<InterceptorAgentRuleBehavior, InterceptorAgentEvent> keyValuePair in InterceptorAgentRule.InvalidActionEventPairs)
			{
				if ((ushort)(evt & keyValuePair.Value) != 0 && action == keyValuePair.Key)
				{
					InterceptorAgentEvent validEventsMask = ~keyValuePair.Value;
					IEnumerable<string> values = from InterceptorAgentEvent val in Enum.GetValues(typeof(InterceptorAgentEvent))
					where (ushort)(val & validEventsMask) != 0
					select val.ToString();
					error = Strings.InterceptorErrorInvalidEventActionPair((evt & keyValuePair.Value).ToString(), keyValuePair.Key.ToString(), string.Join(", ", values));
					return false;
				}
			}
			return true;
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x001A9AF8 File Offset: 0x001A7CF8
		internal static bool TryCreateConditions(string filter, out List<InterceptorAgentCondition> conditions, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			conditions = new List<InterceptorAgentCondition>();
			if (filter.Trim().Contains(" -or "))
			{
				error = Strings.InterceptorErrorConditionConjunctionNotSupported("or");
				return false;
			}
			bool flag = false;
			string header = null;
			InterceptorAgentConditionMatchType headerMatchType = InterceptorAgentConditionMatchType.CaseInsensitive;
			string[] array = filter.Trim().Split(new string[]
			{
				" -and "
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				string text2;
				string text3;
				string operand;
				bool result;
				InterceptorAgentConditionType interceptorAgentConditionType;
				if (!InterceptorHelper.TryMatchCondition(text, out text2, out text3, out operand))
				{
					error = Strings.InterceptorErrorConditionInvalidFormat(text);
					result = false;
				}
				else if (!EnumValidator<InterceptorAgentConditionType>.TryParse(text2, EnumParseOptions.IgnoreCase, out interceptorAgentConditionType) || interceptorAgentConditionType == InterceptorAgentConditionType.Invalid)
				{
					error = Strings.InterceptorErrorConditionInvalidProperty(text2);
					result = false;
				}
				else if (!InterceptorHelper.CheckHeaderValue(flag, interceptorAgentConditionType, out error))
				{
					result = false;
				}
				else
				{
					InterceptorAgentConditionMatchType interceptorAgentConditionMatchType = InterceptorHelper.ParseMatchType(operand);
					string error2;
					if (!InterceptorAgentCondition.ValidateCondition(interceptorAgentConditionType, interceptorAgentConditionMatchType, out error2))
					{
						error = Strings.InterceptorErrorInvalidConditionMatchTypePair(text, error2);
						result = false;
					}
					else
					{
						if (InterceptorAgentCondition.ValidateConditionTypeValue(interceptorAgentConditionType, text3, out error2))
						{
							if (interceptorAgentConditionType == InterceptorAgentConditionType.HeaderName)
							{
								flag = true;
								header = text3;
								headerMatchType = interceptorAgentConditionMatchType;
							}
							else if (interceptorAgentConditionType == InterceptorAgentConditionType.HeaderValue)
							{
								try
								{
									conditions.Add(new InterceptorAgentCondition(header, headerMatchType, text3, interceptorAgentConditionMatchType));
								}
								catch (ArgumentException error3)
								{
									error = Strings.InterceptorErrorConditionInvalidRegex(text3, error3);
									return false;
								}
								flag = false;
								header = null;
							}
							else
							{
								if (interceptorAgentConditionType == InterceptorAgentConditionType.ProcessRole)
								{
									ProcessTransportRole[] processRoles;
									if (!InterceptorAgentCondition.ValidateProcessRole(interceptorAgentConditionMatchType, text3, out processRoles, out error2))
									{
										error = Strings.InterceptorErrorInvalidConditionMatchTypePair(text, error2);
										return false;
									}
									try
									{
										conditions.Add(new InterceptorAgentCondition(processRoles, text3, interceptorAgentConditionMatchType));
										goto IL_259;
									}
									catch (ArgumentException ex)
									{
										error = Strings.InterceptorErrorInvalidConditionMatchTypePair(text, ex.Message);
										return false;
									}
								}
								if (interceptorAgentConditionType == InterceptorAgentConditionType.ServerVersion)
								{
									ServerVersion serverVersion;
									if (!InterceptorAgentCondition.ValidateServerVersion(interceptorAgentConditionMatchType, text3, out serverVersion, out error2))
									{
										error = Strings.InterceptorErrorInvalidConditionMatchTypePair(text, error2);
										return false;
									}
									try
									{
										conditions.Add(new InterceptorAgentCondition(serverVersion, text3, interceptorAgentConditionMatchType));
										goto IL_259;
									}
									catch (ArgumentException ex2)
									{
										error = Strings.InterceptorErrorInvalidConditionMatchTypePair(text, ex2.Message);
										return false;
									}
								}
								try
								{
									conditions.Add(new InterceptorAgentCondition(interceptorAgentConditionType, text3, interceptorAgentConditionMatchType));
								}
								catch (ArgumentException error4)
								{
									error = Strings.InterceptorErrorConditionInvalidRegex(text3, error4);
									return false;
								}
							}
							IL_259:
							i++;
							continue;
						}
						error = Strings.InterceptorErrorInvalidConditionTypeValue(interceptorAgentConditionType.ToString(), text3, error2);
						result = false;
					}
				}
				return result;
			}
			if (flag)
			{
				error = Strings.InterceptorErrorConditionHeaderValueMissing;
				conditions.Clear();
				return false;
			}
			return true;
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x001A9DD8 File Offset: 0x001A7FD8
		internal static bool TryCreateAction(InterceptorAgentRuleBehavior behavior, string customResponseCode, bool responseCodeModified, string customResponseText, bool responseTextModified, TimeSpan timeInterval, string path, out InterceptorAgentAction action, out LocalizedString warning, out LocalizedString error)
		{
			action = null;
			error = LocalizedString.Empty;
			warning = LocalizedString.Empty;
			if (!InterceptorAgentAction.IsValidRuleBehavior(behavior))
			{
				error = Strings.InterceptorErrorInvalidBehavior(behavior.ToString(), string.Join(", ", InterceptorAgentAction.AllActions));
				return false;
			}
			if (behavior == InterceptorAgentRuleBehavior.TransientReject || behavior == InterceptorAgentRuleBehavior.PermanentReject)
			{
				return InterceptorHelper.TryCreateRejectAction(behavior, customResponseCode, responseCodeModified, customResponseText, responseTextModified, out action, out error);
			}
			if ((responseCodeModified || responseTextModified) && !InterceptorAgentAction.IsRejectingBehavior(behavior))
			{
				warning = Strings.InterceptorWarningCustomResponseCodeTextWithoutRejectAction;
			}
			if (behavior == InterceptorAgentRuleBehavior.Delay || behavior == InterceptorAgentRuleBehavior.Defer)
			{
				if (timeInterval <= TimeSpan.Zero || timeInterval >= TimeSpan.FromMilliseconds(2147483647.0))
				{
					error = Strings.InterceptorErrorTimeIntervalInvalid(timeInterval);
					return false;
				}
				action = new InterceptorAgentAction(behavior, timeInterval);
			}
			else if (InterceptorAgentAction.IsArchivingBehavior(behavior))
			{
				if (!string.IsNullOrEmpty(path))
				{
					if (Path.GetInvalidPathChars().Any((char ch) => path.Contains(ch)))
					{
						error = Strings.InterceptorErrorPathInvalidCharacters(path);
						return false;
					}
				}
				if (!string.IsNullOrEmpty(path) && Path.IsPathRooted(path))
				{
					error = Strings.InterceptorErrorRootedPathNotAllowed(path);
					return false;
				}
				SmtpResponse response;
				if (!InterceptorHelper.CreateRejectSmtpResponse(behavior, customResponseCode, responseCodeModified, customResponseText, responseTextModified, out response, out error))
				{
					return false;
				}
				action = new InterceptorAgentAction(behavior, path, response);
			}
			else
			{
				action = new InterceptorAgentAction(behavior);
			}
			if (!string.IsNullOrEmpty(path) && !InterceptorAgentAction.IsArchivingBehavior(behavior))
			{
				warning = Strings.InterceptorWarningPathIgnored;
			}
			return true;
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x001A9F8C File Offset: 0x001A818C
		internal static bool TryGetStatusCodeForModifiedRejectAction(InterceptorAgentRuleBehavior modifiedBehavior, InterceptorAgentRuleBehavior originalBehavior, string originalCustomResponseCode, out string newCustomResponseCode)
		{
			newCustomResponseCode = string.Empty;
			if (originalBehavior == InterceptorAgentRuleBehavior.TransientReject || originalBehavior == InterceptorAgentRuleBehavior.PermanentReject)
			{
				if (modifiedBehavior == InterceptorAgentRuleBehavior.TransientReject && SmtpResponse.InterceptorPermanentlyRejectedMessage.StatusCode.Equals(originalCustomResponseCode))
				{
					newCustomResponseCode = SmtpResponse.InterceptorTransientlyRejectedMessage.StatusCode;
					return true;
				}
				if (modifiedBehavior == InterceptorAgentRuleBehavior.PermanentReject && SmtpResponse.InterceptorTransientlyRejectedMessage.StatusCode.Equals(originalCustomResponseCode))
				{
					newCustomResponseCode = SmtpResponse.InterceptorPermanentlyRejectedMessage.StatusCode;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x001AA000 File Offset: 0x001A8200
		internal static LocalizedString GetArchivedItemRetentionMessage(InterceptorAgentRuleBehavior action, string ruleName, string relativePath, int retentionPeriod)
		{
			string text = Path.Combine(InterceptorAgentSettings.ArchivePath, ((ushort)(action & InterceptorAgentRuleBehavior.Archive) == 32) ? InterceptorAgentSettings.ArchivedMessagesDirectory : InterceptorAgentSettings.ArchivedMessageHeadersDirectory);
			if (!string.IsNullOrEmpty(relativePath))
			{
				text = Path.Combine(text, relativePath);
			}
			return Strings.InterceptorWarningArchivedItemsLifeTime(ruleName, text, retentionPeriod);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x001AA054 File Offset: 0x001A8254
		private static bool TryMatchCondition(string condition, out string name, out string value, out string operand)
		{
			name = null;
			value = null;
			operand = null;
			string[] separator = (from op in InterceptorHelper.OperandList
			select string.Format(" -{0} ", op)).ToArray<string>();
			if (condition.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length != 2)
			{
				return false;
			}
			Match match = InterceptorHelper.conditionPattern.Match(condition);
			if (!match.Success)
			{
				return false;
			}
			Group group = match.Groups["property"];
			Group group2 = match.Groups["operand"];
			Group group3 = match.Groups["value"];
			if (!group.Success || !group2.Success || !group3.Success)
			{
				return false;
			}
			name = group.Value;
			operand = group2.Value.ToLower();
			value = group3.Value;
			return true;
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x001AA12A File Offset: 0x001A832A
		private static bool CheckHeaderValue(bool nextMustBeHeaderValue, InterceptorAgentConditionType property, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (nextMustBeHeaderValue && property != InterceptorAgentConditionType.HeaderValue)
			{
				error = Strings.InterceptorErrorConditionHeaderValueMissing;
				return false;
			}
			if (!nextMustBeHeaderValue && property == InterceptorAgentConditionType.HeaderValue)
			{
				error = Strings.InterceptorErrorConditionHeaderNameMissing;
				return false;
			}
			return true;
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x001AA160 File Offset: 0x001A8360
		private static bool CreateRejectSmtpResponse(InterceptorAgentRuleBehavior behavior, string customResponseCode, bool responseCodeModified, string customResponseText, bool responseTextModified, out SmtpResponse smtpResponse, out LocalizedString error)
		{
			smtpResponse = SmtpResponse.Empty;
			error = LocalizedString.Empty;
			bool flag = (ushort)(behavior & InterceptorAgentRuleBehavior.TransientReject) != 0;
			bool flag2 = (ushort)(behavior & InterceptorAgentRuleBehavior.PermanentReject) != 0;
			if (!flag && !flag2)
			{
				return true;
			}
			if (responseCodeModified || responseTextModified)
			{
				if (responseCodeModified)
				{
					if (string.IsNullOrEmpty(customResponseCode) || customResponseCode.Length != 3)
					{
						error = Strings.InterceptorErrorCustomResponseCodeInvalid(customResponseCode);
						return false;
					}
					int num;
					if (!int.TryParse(customResponseCode, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
					{
						error = Strings.InterceptorErrorCustomResponseCodeInvalid(customResponseCode);
						return false;
					}
					string text;
					if (flag)
					{
						if (num > 499 || num < 400)
						{
							error = Strings.InterceptorErrorCustomResponseCodeMustMatchRejectAction(num, behavior.ToString(), 4);
							return false;
						}
						text = SmtpResponse.InterceptorTransientlyRejectedMessage.StatusText[0];
					}
					else
					{
						if (num > 599 || num < 500)
						{
							error = Strings.InterceptorErrorCustomResponseCodeMustMatchRejectAction(num, behavior.ToString(), 5);
							return false;
						}
						text = SmtpResponse.InterceptorPermanentlyRejectedMessage.StatusText[0];
					}
					if (responseTextModified)
					{
						if (!InterceptorHelper.TryValidateCustomResponseText(customResponseText, out error))
						{
							return false;
						}
						text = customResponseText;
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("{0}.{1}.{2}", customResponseCode[0], customResponseCode[1], customResponseCode[2]);
					smtpResponse = new SmtpResponse(customResponseCode, stringBuilder.ToString(), new string[]
					{
						text
					});
				}
				else
				{
					if (!InterceptorHelper.TryValidateCustomResponseText(customResponseText, out error))
					{
						return false;
					}
					if (flag)
					{
						smtpResponse = new SmtpResponse(SmtpResponse.InterceptorTransientlyRejectedMessage.StatusCode, SmtpResponse.InterceptorTransientlyRejectedMessage.EnhancedStatusCode, new string[]
						{
							customResponseText
						});
					}
					else
					{
						smtpResponse = new SmtpResponse(SmtpResponse.InterceptorPermanentlyRejectedMessage.StatusCode, SmtpResponse.InterceptorPermanentlyRejectedMessage.EnhancedStatusCode, new string[]
						{
							customResponseText
						});
					}
				}
				return true;
			}
			if (flag)
			{
				smtpResponse = SmtpResponse.InterceptorTransientlyRejectedMessage;
				return true;
			}
			smtpResponse = SmtpResponse.InterceptorPermanentlyRejectedMessage;
			return true;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x001AA37C File Offset: 0x001A857C
		private static bool TryCreateRejectAction(InterceptorAgentRuleBehavior behavior, string customResponseCode, bool responseCodeModified, string customResponseText, bool responseTextModified, out InterceptorAgentAction action, out LocalizedString error)
		{
			action = null;
			SmtpResponse response;
			if (!InterceptorHelper.CreateRejectSmtpResponse(behavior, customResponseCode, responseCodeModified, customResponseText, responseTextModified, out response, out error))
			{
				return false;
			}
			action = new InterceptorAgentAction(behavior, response);
			return true;
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x001AA3AC File Offset: 0x001A85AC
		private static bool TryValidateCustomResponseText(string customResponseText, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (string.IsNullOrEmpty(customResponseText))
			{
				error = Strings.InterceptorErrorCustomTextInvalid(customResponseText);
				return false;
			}
			for (int i = 0; i < customResponseText.Length; i++)
			{
				if (customResponseText[i] < '\u0001' || customResponseText[i] > '\u007f')
				{
					error = Strings.InterceptorErrorCustomTextNonAscii(customResponseText);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x001AA410 File Offset: 0x001A8610
		private static InterceptorAgentConditionMatchType ParseMatchType(string operand)
		{
			if (operand != null)
			{
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x600613b-1 == null)
				{
					<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x600613b-1 = new Dictionary<string, int>(12)
					{
						{
							"eq",
							0
						},
						{
							"ieq",
							1
						},
						{
							"ceq",
							2
						},
						{
							"ne",
							3
						},
						{
							"ine",
							4
						},
						{
							"cne",
							5
						},
						{
							"match",
							6
						},
						{
							"like",
							7
						},
						{
							"gt",
							8
						},
						{
							"ge",
							9
						},
						{
							"lt",
							10
						},
						{
							"le",
							11
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x600613b-1.TryGetValue(operand, out num))
				{
					InterceptorAgentConditionMatchType result;
					switch (num)
					{
					case 0:
					case 1:
						result = InterceptorAgentConditionMatchType.CaseInsensitiveEqual;
						break;
					case 2:
						result = InterceptorAgentConditionMatchType.CaseSensitiveEqual;
						break;
					case 3:
					case 4:
						result = InterceptorAgentConditionMatchType.CaseInsensitiveNotEqual;
						break;
					case 5:
						result = InterceptorAgentConditionMatchType.CaseSensitiveNotEqual;
						break;
					case 6:
						result = InterceptorAgentConditionMatchType.Regex;
						break;
					case 7:
						result = InterceptorAgentConditionMatchType.PatternMatch;
						break;
					case 8:
						result = InterceptorAgentConditionMatchType.GreaterThan;
						break;
					case 9:
						result = InterceptorAgentConditionMatchType.GreaterThanOrEqual;
						break;
					case 10:
						result = InterceptorAgentConditionMatchType.LessThan;
						break;
					case 11:
						result = InterceptorAgentConditionMatchType.LessThanOrEqual;
						break;
					default:
						goto IL_129;
					}
					return result;
				}
			}
			IL_129:
			throw new InvalidOperationException("Regex matching should not allow unrecognized match type.");
		}

		// Token: 0x0400364B RID: 13899
		internal const string InterceptorRuleNoun = "InterceptorRule";

		// Token: 0x0400364C RID: 13900
		internal const string NameParameter = "Name";

		// Token: 0x0400364D RID: 13901
		internal const string DescriptionParameter = "Description";

		// Token: 0x0400364E RID: 13902
		internal const string ActionParameter = "Action";

		// Token: 0x0400364F RID: 13903
		internal const string EventParameter = "Event";

		// Token: 0x04003650 RID: 13904
		internal const string ConditionParameter = "Condition";

		// Token: 0x04003651 RID: 13905
		internal const string ServerParameter = "Server";

		// Token: 0x04003652 RID: 13906
		internal const string DagParameter = "Dag";

		// Token: 0x04003653 RID: 13907
		internal const string SiteParameter = "Site";

		// Token: 0x04003654 RID: 13908
		internal const string ExpireTimeParameter = "ExpireTime";

		// Token: 0x04003655 RID: 13909
		internal const string TimeIntervalParameter = "TimeInterval";

		// Token: 0x04003656 RID: 13910
		internal const string CustomResponseCodeParameter = "CustomResponseCode";

		// Token: 0x04003657 RID: 13911
		internal const string CustomResponseTextParameter = "CustomResponseText";

		// Token: 0x04003658 RID: 13912
		internal const string PathParameter = "Path";

		// Token: 0x04003659 RID: 13913
		internal const string SourceParameter = "Source";

		// Token: 0x0400365A RID: 13914
		internal const string CreatedByParameter = "CreatedBy";

		// Token: 0x0400365B RID: 13915
		internal static readonly string[] OperandList = new string[]
		{
			"eq",
			"ieq",
			"ceq",
			"ne",
			"ine",
			"cne",
			"match",
			"like",
			"gt",
			"ge",
			"lt",
			"le"
		};

		// Token: 0x0400365C RID: 13916
		internal static readonly Regex conditionPattern = new Regex(string.Format("^\\s*(?<property>\\w+)\\s* -(?<operand>{0})\\s* (?<quote>\"|\\'|({{))(?<value>.*)(?(1)}}|\\k<quote>)\\s*$", string.Join("|", InterceptorHelper.OperandList)), RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
