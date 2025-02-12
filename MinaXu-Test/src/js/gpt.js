async function callFastGPT(prompt) {
    const apiKey = "huamei-ERTaZXvYuDQesusqaw2WGexoRwAy0GLPpscVS2YqOLFIk2r3UXSfo"; // 你的 FastGPT API Key
    const apiUrl = "https://ai.huameisoft.cn/api/v1/chat/completions";

    const requestBody = {
        chatId: "my_chatId",
        stream: false,
        detail: false,
        responseChatItemId: "my_responseChatItemId",
        variables: {
            uid: "asdfadsfasfd2323",
            name: "张三"
        },
        messages: [
            {
                role: "user",
                content: `请基于我的输入内容【${prompt}】进行解析，并返回符合下述 JSON 结构的内容请仅返回 **纯 JSON**，不要包含 Markdown 代码块（如 \`\`\`json ）。只返回一个json json
                        {
                        "公司名称": "公司名称",
                        "客户名称": "客户联系人，多人用逗号隔开",
                        "跟进状态": "当前状态",
                        "进展总结": "沟通摘要",--
                        "结论总结": "结论总结",
                        "待办总结": "待办事项",
                        "下一步沟通建议": [
                        "建议1",
                        "建议2",
                        "建议3"
                        ]
                        }` // 传入的 prompt 替换 content
            }
        ]
    };

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${apiKey}`
            },
            body: JSON.stringify(requestBody)
        });

        if (!response.ok) {
            throw new Error(`请求失败: ${response.statusText}`);
        }

        const data = await response.json();
        console.log("FastGPT 回复:", data);

        // 提取 "content" 值
        // const content = data.choices[0].message.content;

        const content = '{"公司名称": "上海普嫌包装制品有限公司","客户名称": "顾总, 强总","跟进状态": "跟进中有需求有预算","进展总结": "讨论了业务流程调整、物料编码规则变更以及数据整理计划，客户已安排人员测试数据。","结论总结": "客户同意在12月6日前提供第一轮数据，并确认开发需求的冻结时间。","待办总结": "客户需要确认业务需求文档，安排现场培训，并继续与银行沟通银企直连接口问题。","下一步沟通建议": ["确保现场培训顺利进行","重点讲解数据模板","跟进客户确认业务文档，并持续关注银企直连接口的进展"]}'
                
        const jsonData = JSON.parse(content);

        return jsonData;

    // 访问 JSON 数据
    console.log("公司名称:", jsonData["公司名称"]);
    console.log("客户名称:", jsonData["客户名称"]);
    console.log("跟进状态:", jsonData["跟进状态"]);
    console.log("进展总结:", jsonData["进展总结"]);
    console.log("结论总结:", jsonData["结论总结"]);
    console.log("待办总结:", jsonData["待办总结"]);
    console.log("下一步沟通建议:", jsonData["下一步沟通建议"].join("；"));
        return content;

    } catch (error) {
        console.error("❌ FastGPT 请求失败:", error);
        return "请求失败";
    } 
