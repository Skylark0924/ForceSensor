#include "sys.h"
#include "usart.h"    
#include "delay.h"

u8 usart1_buf[100]={0},usart2_buf[100]={0},usart3_buf[100]={0};
u16 index1=0,index2=0,index3=0,flag1=0,flag2=0,flag3=0;

void uart_init(u32 bound){
GPIO_InitTypeDef GPIO_InitStructure;
USART_InitTypeDef USART_InitStructure;
NVIC_InitTypeDef NVIC_InitStructure;

RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2 |   RCC_APB1Periph_USART3, ENABLE);  
        RCC_APB2PeriphClockCmd(RCC_APB2Periph_USART1|RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB, ENABLE);  //使能USART1，GPIOA时钟

/*************UART1********************/
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9; //PA.9
GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP; //复用推挽输出
GPIO_Init(GPIOA, &GPIO_InitStructure);//初始化GPIOA.9

//USART1_RX   GPIOA.10初始化
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;//PA10
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;//浮空输入
GPIO_Init(GPIOA, &GPIO_InitStructure);//初始化GPIOA.10  

NVIC_InitStructure.NVIC_IRQChannel = USART1_IRQn;
NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority=2 ;//抢占优先级3
NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;      //子优先级3
NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;         //IRQ通道使能
NVIC_Init(&NVIC_InitStructure); //根据指定的参数初始化VIC寄存器


USART_InitStructure.USART_BaudRate = bound;//串口波特率
USART_InitStructure.USART_WordLength = USART_WordLength_8b;//字长为8位数据格式
USART_InitStructure.USART_StopBits = USART_StopBits_1;//一个停止位
USART_InitStructure.USART_Parity = USART_Parity_No;//无奇偶校验位
USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;//无硬件数据流控制
USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx; //收发模式

USART_Init(USART1, &USART_InitStructure); //初始化串口1
USART_ITConfig(USART1, USART_IT_RXNE, ENABLE);//开启串口接受中断
USART_Cmd(USART1, ENABLE);                    //使能串口1 
/***************UART2******************/    
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2; 
GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP; //复用推挽输出
GPIO_Init(GPIOA, &GPIO_InitStructure);//初始化GPIOA.9

GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;//浮空输入
GPIO_Init(GPIOA, &GPIO_InitStructure);//初始化GPIOA.10  

NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;
NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority=2 ;//抢占优先级3
NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;      //子优先级3
NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;         //IRQ通道使能
NVIC_Init(&NVIC_InitStructure); //根据指定的参数初始化VIC寄存器

           //USART 初始化设置

USART_InitStructure.USART_BaudRate = bound;//串口波特率
USART_InitStructure.USART_WordLength = USART_WordLength_8b;//字长为8位数据格式
USART_InitStructure.USART_StopBits = USART_StopBits_1;//一个停止位
USART_InitStructure.USART_Parity = USART_Parity_No;//无奇偶校验位
USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;//无硬件数据流控制
USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx; //收发模式

USART_Init(USART2, &USART_InitStructure); //初始化串口2
USART_ITConfig(USART2, USART_IT_RXNE, ENABLE);//开启串口接受中断
USART_Cmd(USART2, ENABLE);                    //使能串口2 

/****************UART3***********************/  

//USART3_TX   GPIOB.10
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;                  //PB.10
GPIO_InitStructure.GPIO_Speed = GPIO_Speed_10MHz;
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;     //复用推挽输出
GPIO_Init(GPIOB, &GPIO_InitStructure);                              //初始化GPIOB.10

//USART3_RX   GPIOB.11初始化
GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11;                              //PB11
GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   //浮空输入
GPIO_Init(GPIOB, &GPIO_InitStructure);                          //初始化GPIOB.11

//Usart3 NVIC 配置
NVIC_InitStructure.NVIC_IRQChannel = USART3_IRQn;
NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority=1;  //抢占优先级4
NVIC_InitStructure.NVIC_IRQChannelSubPriority = 3;         //子优先级3
NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;    //IRQ通道使能
NVIC_Init(&NVIC_InitStructure);                //根据指定的参数初始化VIC寄存器

//USART3 初始化设置
USART_InitStructure.USART_BaudRate = 115200;      //串口波特率
USART_InitStructure.USART_WordLength = USART_WordLength_8b;                          //字长为8位数据格式
USART_InitStructure.USART_StopBits = USART_StopBits_1;       //一个停止位
USART_InitStructure.USART_Parity = USART_Parity_No;           //无奇偶校验位
USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;      //无硬件数据流控制
USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;                      //收发模式

USART_Init(USART3, &USART_InitStructure);     //初始化串口3
USART_ITConfig(USART3, USART_IT_RXNE, ENABLE);         //开启串口接受中断
USART_Cmd(USART3, ENABLE);
}

void USART1_IRQHandler(void)
{
        u16 code;
        if(USART_GetITStatus(USART1, USART_IT_RXNE) != RESET)
        {   
                USART_ClearITPendingBit(USART1, USART_IT_RXNE);//Removal of receiving interrupt flag
                code=USART_ReceiveData(USART1);
                usart1_buf[index1] = code;
                index1++;       
            if(code == 0x0a)
            {
                index1 = 0;
                flag1 = 1;
            }
        }    
}

void USART2_IRQHandler(void)
{
        u16 code;
        if(USART_GetITStatus(USART2, USART_IT_RXNE) != RESET)
        {   
            USART_ClearITPendingBit(USART2, USART_IT_RXNE);
            code=USART_ReceiveData(USART2);
            usart2_buf[index2] = code;
            index2++;
            if(code == 0x0a)
            {
                index2 = 0;
                flag2 = 1;
            }
        }    
}

void USART3_IRQHandler(void)
{
        u16 code;
        if(USART_GetITStatus(USART3, USART_IT_RXNE) != RESET)
        {   
                USART_ClearITPendingBit(USART3, USART_IT_RXNE);
                code=USART_ReceiveData(USART3);
                usart3_buf[index3] = code;
                index3++;
                if(code == 0x0a)
                {
                    index3 = 0;
                    flag3 = 1;
                }
        }    
}
void USART1_Send(u8 *str)
{
        while(*str!=0x0A)
        {
        USART_GetFlagStatus(USART1, USART_FLAG_TC);
        USART_SendData(USART1, *str++);
        while( USART_GetFlagStatus(USART1,USART_FLAG_TC)!= SET);
        }
        USART_SendData(USART1, 0x0a);
}

void USART2_Send(u8 *str)
{
        while(*str!=0x0A)
        {
            USART_GetFlagStatus(USART2, USART_FLAG_TC);
            USART_SendData(USART2, *str++);
            while( USART_GetFlagStatus(USART2,USART_FLAG_TC)!= SET);    
        }
        USART_SendData(USART2, 0x0a);
}
void USART3_Send(u8 *str)
{
        while(*str!=0x0A)
        {
            USART_GetFlagStatus(USART3, USART_FLAG_TC);
            USART_SendData(USART3, *str++);
            while( USART_GetFlagStatus(USART3,USART_FLAG_TC)!= SET);    
        }
        USART_SendData(USART3, 0x0a);
}
