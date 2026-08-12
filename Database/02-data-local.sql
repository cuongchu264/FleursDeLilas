-- Local Seed Data for Fleurs de Lilas Studio

-- 1. Seed User Accounts (Hashed Password Sample)
INSERT INTO fleurs_user (username, password) VALUES
('admin_lilas', '$2a$12$eImiTXuWVxfM37uY4JANjOL.88B./cQ.T77e8.rWwE7qU6y3/o4q.'),
('staff_mai', '$2a$12$eImiTXuWVxfM37uY4JANjOL.88B./cQ.T77e8.rWwE7qU6y3/o4q.'),
('staff_nam', '$2a$12$eImiTXuWVxfM37uY4JANjOL.88B./cQ.T77e8.rWwE7qU6y3/o4q.');

-- 2. Seed Flower Inventory (flower)
INSERT INTO flower (flo_name, flo_price, flo_toal_count, flo_avaiable_count, flo_failed_count, flo_buy_date, flo_note) VALUES
('Red Ecuador Rose', 35, 100, 85, 15, '2026-08-08 07:00:00+07', 'Fresh import from night flower market, large blooms'),
('Purple Dutch Tulip', 45, 60, 50, 10, '2026-08-09 08:30:00+07', 'Stored in cold room at 15C'),
('Blue Hydrangea', 60, 40, 38, 2, '2026-08-09 08:30:00+07', 'Requires continuous misting'),
('White Dutch Baby Breath', 25, 80, 75, 5, '2026-08-07 06:00:00+07', 'Used as filler flowers for main bouquet'),
('Da Lat Sunflower', 20, 50, 45, 5, '2026-08-10 06:30:00+07', 'Thick stem, fresh branch');

-- 3. Seed Supply Inventory (supply)
INSERT INTO supply (sup_name, sup_price, sup_count, sup_buy_date, sup_note) VALUES
('Korean Sand White Wrapping Paper', 15, 200, '2026-08-08 07:00:00+07', 'Waterproof quality material'),
('Cream Silk Ribbon', 8, 150, '2026-08-08 07:00:00+07', 'Satin ribbon 4cm width'),
('Round Rattan Wooden Basket Size M', 55, 30, '2026-08-09 09:00:00+07', 'Premium basket for grand opening arrangements'),
('Fleurs de Lilas Greeting Card', 5, 500, '2026-08-07 06:00:00+07', 'Printed studio logo card attached to flowers'),
('Oasis Floral Foam', 12, 100, '2026-08-09 08:30:00+07', 'Fast water-absorbing foam');

-- 4. Seed Orders (fleurs_order)
INSERT INTO fleurs_order (order_name, order_price, order_ship_price, order_date) VALUES
('Rose Bouquet for Ms. Linh Birthday', 650, 30, '2026-08-10 09:30:00+07'),
('Hydrangea Floral Basket for Grand Opening', 1200, 50, '2026-08-10 10:15:00+07'),
('Purple Tulip Bouquet for Wedding Anniversary', 850, 0, '2026-08-10 14:00:00+07');

-- 5. Order Flower Details (order_prepare_flo)
-- Order 1 (Rose Bouquet): 10 Ecuador Roses + 3 Baby Breaths
INSERT INTO order_prepare_flo (flo_id, order_id, order_pre_flo_count, order_pre_flo_note) VALUES
(1, 1, 10, 'Select medium-bloomed roses'),
(4, 1, 3, 'Accent around the rose bouquet');

-- Order 2 (Hydrangea Basket): 8 Hydrangeas + 10 Sunflowers + 5 Ecuador Roses
INSERT INTO order_prepare_flo (flo_id, order_id, order_pre_flo_count, order_pre_flo_note) VALUES
(3, 2, 8, 'Centerpiece arrangement'),
(5, 2, 10, 'Arrange tall for depth creation'),
(1, 2, 5, 'Fill at the base of basket');

-- Order 3 (Tulip Bouquet): 12 Purple Tulips
INSERT INTO order_prepare_flo (flo_id, order_id, order_pre_flo_count, order_pre_flo_note) VALUES
(2, 3, 12, 'Round bouquet in Korean style');

-- 6. Order Supply Details (order_prepare_suplly)
-- Order 1: 2 Wrapping Papers + 1 Ribbon + 1 Greeting Card
INSERT INTO order_prepare_suplly (sup_id, order_id, order_pre_up_count, order_pre_up_note) VALUES
(1, 1, 2, 'Elegant tall wrapping style'),
(2, 1, 1, 'Double bow tie'),
(4, 1, 1, 'Printed wish: Happy Birthday my dear sister!');

-- Order 2: 1 Wooden Basket + 2 Foams + 1 Greeting Card
INSERT INTO order_prepare_suplly (sup_id, order_id, order_pre_up_count, order_pre_up_note) VALUES
(3, 2, 1, 'Clean rattan basket'),
(5, 2, 2, 'Fully soak in water before arranging'),
(4, 2, 1, 'Printed wish: Congratulations to ABC Company');

-- Order 3: 2 Wrapping Papers + 1 Ribbon + 1 Greeting Card
INSERT INTO order_prepare_suplly (sup_id, order_id, order_pre_up_count, order_pre_up_note) VALUES
(1, 3, 2, 'Cream white paper wrapping'),
(2, 3, 1, 'Cream silk ribbon'),
(4, 3, 1, 'Handwritten greeting card');