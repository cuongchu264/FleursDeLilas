-- Schema for Fleurs de Lilas Studio Store (PostgreSQL)

-- 1. User Table (Users / Administrators)
CREATE TABLE IF NOT EXISTS fleurs_user (
    id SERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. Flower Table (Flower Management)
CREATE TABLE IF NOT EXISTS flower (
    id SERIAL PRIMARY KEY,
    flo_name VARCHAR(255) NOT NULL,
    flo_price BIGINT NOT NULL DEFAULT 0,
    flo_toal_count INT NOT NULL DEFAULT 0,
    flo_avaiable_count INT NOT NULL DEFAULT 0,
    flo_failed_count INT NOT NULL DEFAULT 0,
    flo_buy_date TIMESTAMP WITH TIME ZONE,
    flo_note VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 3. Supply Table (Supplies / Accessories like wrapping paper, ribbons, baskets...)
CREATE TABLE IF NOT EXISTS supply (
    id SERIAL PRIMARY KEY,
    sup_name VARCHAR(255) NOT NULL,
    sup_price BIGINT NOT NULL DEFAULT 0,
    sup_count INT NOT NULL DEFAULT 0,
    sup_note VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 4. Order Table (Orders)
CREATE TABLE IF NOT EXISTS fleurs_order (
    id SERIAL PRIMARY KEY,
    order_name VARCHAR(255) NOT NULL,
    order_price BIGINT NOT NULL DEFAULT 0,
    order_ship_price BIGINT NOT NULL DEFAULT 0,
    order_date TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 5. Order_Prepare_Flo Table (Flower preparation details for Orders)
CREATE TABLE IF NOT EXISTS order_prepare_flo (
    id SERIAL PRIMARY KEY,
    flo_id INT NOT NULL REFERENCES flower(id) ON DELETE CASCADE,
    order_id INT NOT NULL REFERENCES fleurs_order(id) ON DELETE CASCADE,
    order_pre_flo_count INT NOT NULL DEFAULT 1,
    order_pre_flo_note VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 6. Order_Prepare_Suplly Table (Supply preparation details for Orders)
CREATE TABLE IF NOT EXISTS order_prepare_suplly (
    id SERIAL PRIMARY KEY,
    sup_id INT NOT NULL REFERENCES supply(id) ON DELETE CASCADE,
    order_id INT NOT NULL REFERENCES fleurs_order(id) ON DELETE CASCADE,
    order_pre_up_count INT NOT NULL DEFAULT 1,
    order_pre_up_note VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);