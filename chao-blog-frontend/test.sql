SELECT    t.*,
          d.wuliao_id xiaotu
FROM      (
                 SELECT u.id,
                        u.sale_list_id       sid,
                        u.sheng_chan_id      cid,
                        s.wuliao_id          datu,
                        s.sale_number        salenumber,
                        s.xiangmu_id         xiangmuhao,
                        p.NAME               gongxu,
                        g.process_group      gongxuzu,
                        p.id                 process_id,
                        u.resultnum          num,
                        c.num                sumnum,
                        u.resultzbgongshi    zbgongshi,
                        u.resultczgongshi    czgongshi,
                        u.resultinall        inall,
                        u1.true_name         USER,
                        u.date_start_product datestartproduct,
                        u.date_in_product    dateinproduct,
                        s.refer_date         handoverdate,
                        u.drawing_id
                 FROM   (
                                 SELECT   *,
                                          Sum(Ifnull(num,semifinished) * ratio)                                                         resultnum,
                                          Sum(zb_gong_shi              *ratio)                                                          resultzbgongshi,
                                          Sum(Round(Ifnull(num,semifinished) * cz_gong_shi*ratio,2))                                    resultczgongshi,
                                          Sum(Round((Ifnull(zb_gong_shi,0) +(Ifnull(cz_gong_shi,0)*Ifnull(num,semifinished)))*ratio,2)) resultinall
                                 FROM     t_user_product
                                 WHERE    true " + aparmSql3 + "
                                 GROUP BY sheng_chan_id
                                 HAVING   resultnum < 0
                                 ORDER BY sale_list_id,
                                          sheng_chan_id,
                                          id) as u,
                        t_sheng_chan as c,
                        (
                               SELECT *
                               FROM   t_sale_list
                               WHERE  true ~) s,
                        (
                               SELECT *
                               FROM   t_user
                               WHERE  true ~) u1,
                        (
                               SELECT *
                               FROM   t_process
                               WHERE  true ~) p,
                        t_process_group g
                 WHERE  u.sheng_chan_id = c.id
                 AND    u.sale_list_id = s.id
                 AND    u.user_id = u1.id
                 AND    u.process_id = p.id
                 AND    p.process_group_id = g.id) t
LEFT JOIN t_drawing d
ON        t.drawing_id = d.id
ORDER BY  t.sid,
          t.cid,
          t.id