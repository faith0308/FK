/*
SQLyog Ultimate v12.5.0 (64 bit)
MySQL - 8.0.11 : Database - learndb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`learndb` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `learndb`;

/*Table structure for table `orders` */

DROP TABLE IF EXISTS `orders`;

CREATE TABLE `orders` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '订单自增属性',
  `UserId` bigint(20) NOT NULL COMMENT '用户Id',
  `ProductId` bigint(20) NOT NULL COMMENT '产品Id',
  `Price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '产品价格',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `orders` */

/*Table structure for table `products` */

DROP TABLE IF EXISTS `products`;

CREATE TABLE `products` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '产品标识自增',
  `ProductName` varchar(128) NOT NULL COMMENT '产品名称',
  `Description` varchar(200) DEFAULT NULL COMMENT '产品描述',
  `ProductPrice` decimal(10,0) DEFAULT '0' COMMENT '产品价格',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `products` */

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '标识自增属性',
  `UserName` varchar(20) NOT NULL COMMENT '用户账户',
  `UserPwd` varchar(128) NOT NULL COMMENT '用户密码',
  `Six` int(11) NOT NULL COMMENT '性别',
  `Birthday` datetime DEFAULT NULL COMMENT '生日',
  `CreateTime` datetime NOT NULL COMMENT '创建时间',
  `Remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `Status` int(11) NOT NULL COMMENT '状态 1-有效，0-失效',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `users` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
