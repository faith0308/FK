using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CPlatform.Swagger
{
    public class SwaggerXmlMerge
    {
        public static void SwaggerMerge()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            // 找到所有的注释文档， 根据你的项目情况加上通配符。
            var files = Directory.GetFiles(directory, "*.xml").ToList();
            // 找到你的主项目注释文档
            var mainFile = files.Where(u => u.Contains("Main.xml")).FirstOrDefault();
            if (string.IsNullOrEmpty(mainFile))
            {
                var filePath = Path.Combine(directory, "Main.xml");
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfo.Create();
                //重新加载
                files = Directory.GetFiles(directory, "*.xml").ToList();
                mainFile = files.Where(u => u.Contains("Main.xml")).FirstOrDefault();
            }
            else
            {
                files.Remove(mainFile);
            }
            if (files.Count == 0)
                return;
            var mainDoc = new XmlDocument();
            mainDoc.Load(mainFile);
            var docNode = mainDoc.SelectSingleNode("//doc");
            var membersNode = mainDoc.SelectSingleNode("//members");
            docNode.RemoveChild(membersNode);
            XmlElement members = mainDoc.CreateElement("members");
            docNode.AppendChild(members);
            files.ForEach(file =>
            {
                var doc = new XmlDocument();
                doc.Load(file);
                var nodes = doc.SelectNodes("//member");
                foreach (XmlNode node in nodes)
                {
                    var importNode = mainDoc.ImportNode(node, true);
                    members.AppendChild(importNode);
                }
            });
            files.ForEach(u =>
            {
                File.Delete(u);
            });
            mainDoc.Save(mainFile);
        }
    }
}
