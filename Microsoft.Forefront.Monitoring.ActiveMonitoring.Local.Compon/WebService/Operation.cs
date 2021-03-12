using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000287 RID: 647
	public class Operation
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00041290 File Offset: 0x0003F490
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00041298 File Offset: 0x0003F498
		internal TimeSpan Sla
		{
			get
			{
				return this.sla;
			}
			set
			{
				this.sla = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x000412A1 File Offset: 0x0003F4A1
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x000412A9 File Offset: 0x0003F4A9
		internal int MaxRetryAttempts
		{
			get
			{
				return this.maxRetryAttempts;
			}
			set
			{
				this.maxRetryAttempts = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000412B2 File Offset: 0x0003F4B2
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x000412BA File Offset: 0x0003F4BA
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000412C3 File Offset: 0x0003F4C3
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x000412CB File Offset: 0x0003F4CB
		internal List<Parameter> Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x000412D4 File Offset: 0x0003F4D4
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x000412DC File Offset: 0x0003F4DC
		internal Type ReturnType
		{
			get
			{
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x000412E5 File Offset: 0x0003F4E5
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x000412ED File Offset: 0x0003F4ED
		internal List<ResultItem> ExpectedResultItems
		{
			get
			{
				return this.expectedResultItems;
			}
			set
			{
				this.expectedResultItems = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000412F6 File Offset: 0x0003F4F6
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x000412FE File Offset: 0x0003F4FE
		internal object Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00041307 File Offset: 0x0003F507
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x0004130F File Offset: 0x0003F50F
		internal TimeSpan PauseTime
		{
			get
			{
				return this.pauseTime;
			}
			set
			{
				this.pauseTime = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00041318 File Offset: 0x0003F518
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x00041320 File Offset: 0x0003F520
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00041329 File Offset: 0x0003F529
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00041331 File Offset: 0x0003F531
		internal TimeSpan Latency { get; set; }

		// Token: 0x06001537 RID: 5431 RVA: 0x0004133C File Offset: 0x0003F53C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Operation: {0}", this.Name));
			foreach (Parameter parameter in this.Parameters)
			{
				stringBuilder.AppendLine(string.Format("Parameter: {0}", parameter.ToString()));
			}
			foreach (ResultItem resultItem in this.ExpectedResultItems)
			{
				stringBuilder.AppendLine(string.Format("ResultItem: {0}", resultItem.ToString()));
			}
			stringBuilder.AppendLine(string.Format("SLA: {0}", this.Sla));
			stringBuilder.AppendLine(string.Format("ID: {0}", this.Id));
			stringBuilder.AppendLine(string.Format("PauseTime: {0}", this.PauseTime));
			string arg = (this.ReturnType == null) ? string.Empty : this.ReturnType.ToString();
			string arg2 = (this.Result == null) ? string.Empty : Utils.SerializeToXml(this.Result);
			stringBuilder.AppendLine(string.Format("Return type: {0}", arg));
			stringBuilder.AppendLine(string.Format("Return Value: {0}", arg2));
			return stringBuilder.ToString();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000414C8 File Offset: 0x0003F6C8
		internal bool Invoke(WebServiceClient client, List<Operation> operations, bool throwIfSlaMissed = true)
		{
			if (client == null || operations == null || operations.Count == 0)
			{
				throw new ArgumentNullException();
			}
			object proxy = client.Proxy;
			MethodInfo method = proxy.GetType().GetMethod(this.Name);
			if (method == null)
			{
				throw new ArgumentException(string.Format("Operation '{0}' not found in proxy class type '{1}'.", this.Name, proxy.GetType()));
			}
			object[] array = this.GetParameters(client.Assembly, operations);
			int num = (array == null) ? 0 : array.Length;
			int num2 = method.GetParameters().Count<ParameterInfo>();
			if (num != num2)
			{
				throw new Exception(string.Format("Work definition error - incorrect number of parameters '{0}' in Operation '{1}'; expecting '{2}'", num, this.Name, num2));
			}
			DateTime utcNow = DateTime.UtcNow;
			object obj;
			try
			{
				obj = method.Invoke(proxy, array);
			}
			catch (CommunicationException)
			{
				client.Abort();
				throw;
			}
			catch (TimeoutException)
			{
				client.Abort();
				throw;
			}
			catch (TargetInvocationException)
			{
				client.Abort();
				throw;
			}
			this.ReturnType = method.ReturnType;
			this.Result = obj;
			this.Latency = DateTime.UtcNow.Subtract(utcNow);
			bool flag = true;
			if (this.Latency.CompareTo(this.Sla) > 0)
			{
				flag = false;
				if (throwIfSlaMissed)
				{
					throw new Exception(string.Format("Operation {0} response time ({1}) exceeded SLA ({2})", this.Name, this.Latency, this.Sla));
				}
			}
			return flag;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00041644 File Offset: 0x0003F844
		internal bool ValidateResult(WebServiceClient client, bool throwIfFailed = true)
		{
			bool flag = true;
			foreach (ResultItem resultItem in this.ExpectedResultItems)
			{
				StringBuilder stringBuilder = this.CreateErrorMessage(resultItem);
				string text;
				XElement n;
				this.PreProcessResult(resultItem, out text, out n);
				switch (resultItem.VerifyMethod)
				{
				case ResultVerifyMethod.ReturnType:
					if (!this.ReturnType.Name.Equals(resultItem.Value, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
						if (throwIfFailed)
						{
							stringBuilder.AppendLine("Type returned:");
							stringBuilder.AppendLine(this.ReturnType.Name);
							stringBuilder.AppendLine("Expected:");
							stringBuilder.AppendLine(resultItem.Value);
							throw new Exception(stringBuilder.ToString());
						}
					}
					break;
				case ResultVerifyMethod.ReturnValue:
					if (resultItem.Index < 0)
					{
						flag = this.Result.ToString().Equals(resultItem.Value, StringComparison.OrdinalIgnoreCase);
					}
					else
					{
						object obj = ((object[])this.Result)[resultItem.Index];
						flag = obj.ToString().Equals(resultItem.Value, StringComparison.OrdinalIgnoreCase);
					}
					if (!flag && throwIfFailed)
					{
						stringBuilder.AppendLine("Value returned:");
						stringBuilder.AppendLine(text);
						stringBuilder.AppendLine("Expected:");
						stringBuilder.AppendLine(resultItem.Value);
						throw new Exception(stringBuilder.ToString());
					}
					break;
				case ResultVerifyMethod.ReturnValueRegex:
				case ResultVerifyMethod.PropertyValueRegex:
				{
					MatchCollection matchCollection = Regex.Matches(text, resultItem.Value, RegexOptions.IgnoreCase);
					if (matchCollection.Count == 0)
					{
						flag = false;
						if (throwIfFailed)
						{
							stringBuilder.AppendLine("Value returned:");
							stringBuilder.AppendLine(text);
							stringBuilder.AppendLine("Regex:");
							stringBuilder.AppendLine(resultItem.Value);
							throw new Exception(stringBuilder.ToString());
						}
					}
					break;
				}
				case ResultVerifyMethod.ReturnValueContains:
				case ResultVerifyMethod.PropertyValueContains:
					if (!text.ToLower().Contains(resultItem.Value.ToLower()))
					{
						flag = false;
						if (throwIfFailed)
						{
							stringBuilder.AppendLine("Value returned:");
							stringBuilder.AppendLine(text);
							stringBuilder.AppendLine("Substring expected:");
							stringBuilder.AppendLine(resultItem.Value);
							throw new Exception(stringBuilder.ToString());
						}
					}
					break;
				case ResultVerifyMethod.ReturnValueXml:
				case ResultVerifyMethod.PropertyValueXml:
				{
					XElement xelement = resultItem.UseFile ? XElement.Load(resultItem.Value) : XElement.Parse(resultItem.Value);
					if (!XNode.DeepEquals(xelement, n))
					{
						flag = false;
						if (throwIfFailed)
						{
							stringBuilder.AppendLine("Value returned:");
							stringBuilder.AppendLine(text);
							stringBuilder.AppendLine("Expected:");
							stringBuilder.AppendLine(xelement.ToString());
							throw new Exception(stringBuilder.ToString());
						}
					}
					break;
				}
				case ResultVerifyMethod.ReturnValueUseValidator:
				{
					object obj2 = Utils.DeserializeFromXml(resultItem.Value, this.Result.GetType());
					MethodInfo validatorMethod = client.GetValidatorMethod(this.Result.GetType());
					object[] array = new object[3];
					array[0] = obj2;
					array[1] = this.Result;
					object[] array2 = array;
					flag = (bool)validatorMethod.Invoke(null, array2);
					if (!flag && throwIfFailed)
					{
						string value = array2[2] as string;
						stringBuilder.AppendLine(value);
						throw new Exception(stringBuilder.ToString());
					}
					break;
				}
				case ResultVerifyMethod.PropertyValue:
					if (resultItem.Index < 0)
					{
						flag = this.ReturnType.GetProperty(resultItem.PropertyName).GetValue(this.Result, null).ToString().Equals(resultItem.Value, StringComparison.OrdinalIgnoreCase);
					}
					else
					{
						object value2 = this.ReturnType.GetProperty(resultItem.PropertyName).GetValue(this.Result, null);
						flag = ((object[])value2)[resultItem.Index].ToString().Equals(resultItem.Value, StringComparison.OrdinalIgnoreCase);
					}
					if (!flag && throwIfFailed)
					{
						stringBuilder.AppendLine("Value returned:");
						stringBuilder.AppendLine(text);
						stringBuilder.AppendLine("Expected:");
						stringBuilder.AppendLine(resultItem.Value);
						throw new Exception(stringBuilder.ToString());
					}
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00041A80 File Offset: 0x0003FC80
		internal string GetDiagnosticsInfo(WebServiceClient client)
		{
			MethodInfo diagnosticsInfoMethod = client.GetDiagnosticsInfoMethod();
			object[] array = new object[]
			{
				this.Result
			};
			return string.Format("{0}:{1}\n", this.Name, (string)diagnosticsInfoMethod.Invoke(null, array));
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00041AE4 File Offset: 0x0003FCE4
		private object[] GetParameters(Assembly assembly, List<Operation> operations)
		{
			if (this.Parameters.Count == 0)
			{
				return null;
			}
			List<object> list = new List<object>();
			using (List<Parameter>.Enumerator enumerator = this.Parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Parameter p = enumerator.Current;
					object item2 = null;
					if (!p.IsNull)
					{
						if (string.IsNullOrWhiteSpace(p.UseResultFromOperationId))
						{
							if (string.IsNullOrWhiteSpace(p.Type))
							{
								item2 = p.Value.InnerText;
							}
							else
							{
								Type type;
								try
								{
									type = Type.GetType(p.Type, true, true);
								}
								catch
								{
									type = assembly.GetType(p.Type, true, true);
								}
								if (p.UseFile)
								{
									XmlDocument xmlDocument = new XmlDocument();
									xmlDocument.Load(p.Value.InnerText);
									item2 = Utils.DeserializeFromXml(xmlDocument.DocumentElement.OuterXml, type);
								}
								else
								{
									item2 = Utils.DeserializeFromXml(p.Value.InnerXml, type);
								}
							}
						}
						else
						{
							Operation operation = operations.Find((Operation item) => item.Id.Equals(p.UseResultFromOperationId, StringComparison.OrdinalIgnoreCase));
							if (string.IsNullOrWhiteSpace(p.PropertyName))
							{
								if (p.Index < 0)
								{
									item2 = operation.Result;
								}
								else
								{
									string elementName = string.Format("The return value of previous Operation '{0}'", operation.Name);
									item2 = this.GetIndexedObject(operation.Result, p.Index, elementName);
								}
							}
							else
							{
								PropertyInfo property = operation.Result.GetType().GetProperty(p.PropertyName);
								if (property == null)
								{
									throw new ArgumentException(string.Format("The result of Operation '{0}' does not have a Property called '{1}'", operation.Name, p.PropertyName));
								}
								object value = property.GetValue(operation.Result, null);
								if (p.Index < 0)
								{
									item2 = value;
								}
								else
								{
									string elementName = string.Format("The value of Property '{0}' in the return value of previous Operation '{1}'", p.PropertyName, operation.Name);
									item2 = this.GetIndexedObject(value, p.Index, elementName);
								}
							}
						}
					}
					list.Add(item2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00041D94 File Offset: 0x0003FF94
		private void PreProcessResult(ResultItem r, out string resultXml, out XElement resultXElement)
		{
			resultXml = null;
			resultXElement = null;
			switch (r.VerifyMethod)
			{
			case ResultVerifyMethod.ReturnValue:
			case ResultVerifyMethod.ReturnValueRegex:
			case ResultVerifyMethod.ReturnValueContains:
			case ResultVerifyMethod.ReturnValueXml:
			case ResultVerifyMethod.ReturnValueIsNotNull:
			case ResultVerifyMethod.ReturnValueUseValidator:
			{
				if (this.Result == null)
				{
					throw new Exception(string.Format("The return value from Operation '{0}' is null; expected non-null result", this.Name));
				}
				if (r.Index < 0)
				{
					resultXml = Utils.SerializeToXml(this.Result);
					resultXElement = Utils.SerializeToXmlElement(this.Result);
					return;
				}
				string elementName = string.Format("The return value of Operation '{0}'", this.Name);
				object indexedObject = this.GetIndexedObject(this.Result, r.Index, elementName);
				resultXml = Utils.SerializeToXml(indexedObject);
				resultXElement = Utils.SerializeToXmlElement(indexedObject);
				return;
			}
			case ResultVerifyMethod.ReturnValueIsNull:
				if (this.Result != null)
				{
					throw new Exception(string.Format("The return value of Operation '{0}' is not null; expected null result", this.Name));
				}
				break;
			case ResultVerifyMethod.PropertyValue:
			case ResultVerifyMethod.PropertyValueRegex:
			case ResultVerifyMethod.PropertyValueContains:
			case ResultVerifyMethod.PropertyValueXml:
			{
				if (this.Result == null)
				{
					throw new Exception(string.Format("The return value of Operation '{0}' is null; expected non-null result", this.Name));
				}
				PropertyInfo property = this.ReturnType.GetProperty(r.PropertyName);
				if (property == null)
				{
					throw new Exception(string.Format("The return value of Operation '{0}' does not have a property called '{1}':\r\n{2}", this.Name, r.PropertyName, Utils.SerializeToXml(this.Result)));
				}
				object value = property.GetValue(this.Result, null);
				if (value == null)
				{
					throw new Exception(string.Format("The value of Property '{0}' in the return value of Operation '{1}' is null; expected non-null value '{2}'", r.PropertyName, this.Name, r.Value));
				}
				if (r.Index < 0)
				{
					resultXml = Utils.SerializeToXml(value);
					resultXElement = Utils.SerializeToXmlElement(value);
					return;
				}
				string elementName = string.Format("The value of Property '{0}' in the return value of Operation '{1}'", r.PropertyName, this.Name);
				object indexedObject2 = this.GetIndexedObject(value, r.Index, elementName);
				resultXml = Utils.SerializeToXml(indexedObject2);
				resultXElement = Utils.SerializeToXmlElement(indexedObject2);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00041F64 File Offset: 0x00040164
		private object GetIndexedObject(object o, int index, string elementName)
		{
			if (!(o is Array))
			{
				throw new Exception(string.Format("{0} is not an arry, required index={1}.", elementName, index));
			}
			object[] array = (object[])o;
			int num = array.Length;
			if (index >= num)
			{
				throw new Exception(string.Format("{0} has array length={1}, index '{2}' is out of range.", elementName, num, index));
			}
			return array[index];
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00041FC0 File Offset: 0x000401C0
		private StringBuilder CreateErrorMessage(ResultItem r)
		{
			string text = (r.Index < 0) ? "n/a" : r.Index.ToString();
			string text2 = string.IsNullOrWhiteSpace(r.PropertyName) ? "n/a" : r.PropertyName;
			string value = string.Format("'{0}' validation of operation '{1}' failed - index='{2}', propertyName='{3}'.", new object[]
			{
				r.VerifyMethod.ToString(),
				this.Name,
				text,
				text2
			});
			return new StringBuilder(value);
		}

		// Token: 0x04000A47 RID: 2631
		private TimeSpan sla;

		// Token: 0x04000A48 RID: 2632
		private string name;

		// Token: 0x04000A49 RID: 2633
		private List<Parameter> parameters;

		// Token: 0x04000A4A RID: 2634
		private Type returnType;

		// Token: 0x04000A4B RID: 2635
		private List<ResultItem> expectedResultItems;

		// Token: 0x04000A4C RID: 2636
		private object result;

		// Token: 0x04000A4D RID: 2637
		private TimeSpan pauseTime;

		// Token: 0x04000A4E RID: 2638
		private string id;

		// Token: 0x04000A4F RID: 2639
		private int maxRetryAttempts;
	}
}
